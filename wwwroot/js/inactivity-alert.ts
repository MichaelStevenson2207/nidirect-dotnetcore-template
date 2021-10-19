declare var moment: typeof import("moment");

const secondsToMilliseconds = 1000;
const defaultSessionTimeout = (20 * 60);
const defaultPopupSessionTimeout = (18 * 60);

class InactivityAlert {
    timeoutForModal: any;
    timeoutForSession: any;
    sessionSeconds: number;
    showAfterSeconds: number;
    elExtend: JQuery;
    elDestroy: JQuery;
    elMessage: JQuery;
    intervalToUpdate: number;
    timeoutApiUrl: string;
    elAccessibilityMessage: JQuery<HTMLElement>;

    constructor(sessionSeconds: number, showAfterSeconds: number, timeoutApiUri: string) {
        this.timeoutForModal = null;
        this.timeoutForSession = null;

        this.sessionSeconds = sessionSeconds;
        this.showAfterSeconds = showAfterSeconds;
        this.timeoutApiUrl = timeoutApiUri;
        this.elExtend = $('#extend');
        this.elDestroy = $('#destroy');
        this.elMessage = $('#expiring-in-message');
        this.elAccessibilityMessage = $('#aria-timeout-label');
        this.init = this.init.bind(this);
        this.destroy = this.destroy.bind(this);
        this.restartCounters = this.restartCounters.bind(this);
        this.init();
    }

    static getTimeoutUrl() {
        return $(document.body).data("timeout-url") || "/helper/TimeoutResult";
    }

    static navigateAway() {
        window.location.href = InactivityAlert.getTimeoutUrl();
    }

    setSessionTimeout() {
        this.timeoutForSession = window
            .setTimeout(InactivityAlert.navigateAway, this.sessionSeconds * secondsToMilliseconds);
    }

    setTimeoutForModal() {
        const el: any = $('#timeout-dialog');
        let self = this;
        console.log("Should trigger after " + this.showAfterSeconds);


        this.setAccessibilityMessage();

        this.timeoutForModal = window
            .setTimeout(() => {
                console.log("Initiating the timeout dialog");
                const count = 1000;


                let startTime = (self.sessionSeconds - self.showAfterSeconds) * secondsToMilliseconds;
                const splitMessage = this.elMessage.length ? this.elMessage.html().split(/ [0-9:]+ /) : '';

                this.detachHandlers();

                const updateMessage = function uM() {
                    if (this.elMessage.length) {
                        const formatted = moment.utc(startTime).format('m:ss');
                        this.elMessage.html(`${splitMessage[0]} ${formatted} ${splitMessage[1]}`);
                    }
                    startTime -= count;
                }.bind(this);

                updateMessage();
                this.intervalToUpdate = window.setInterval(updateMessage, count);

                sessionStorage.setItem("ReturnFocus", document.activeElement.id);

                el.modal({
                    escapeClose: false,
                    clickClose: false,
                    showClose: false,
                });
            }, this.showAfterSeconds * secondsToMilliseconds);
    }

    setAccessibilityMessage() {
        const splitMessage = this.elAccessibilityMessage.length ? this.elAccessibilityMessage.html().split(/ [0-9:]+ /) : '';
        let totalTime = (this.sessionSeconds - this.showAfterSeconds) * secondsToMilliseconds;
        this.elAccessibilityMessage.html(`${splitMessage[0]} ${moment.utc(totalTime).format('m')} ${splitMessage[1]}`);
    }

    startCountdown() {
        this.setTimeoutForModal();
        this.setSessionTimeout();
    }

    stopAllCounters() {
        window.clearTimeout(this.timeoutForModal);
        window.clearTimeout(this.timeoutForSession);
        window.clearInterval(this.intervalToUpdate);
    }

    restartCounters() {
        this.stopAllCounters();
        this.startCountdown();
        return true;
    }

    callWebApi() {
        if (this.timeoutApiUrl !== null && this.timeoutApiUrl !== "") {
            $.ajax({
                type: "GET",
                url: this.timeoutApiUrl
            }).done(
                function (result) {
                    console.log('User chose to extend session.');

                    if (sessionStorage.getItem("ReturnFocus") != null && sessionStorage.getItem("ReturnFocus").length > 0) {
                        document.getElementById(sessionStorage.getItem("ReturnFocus")).focus();
                    }
                });
        }
    }

    init() {
        let self = this;
        this.startCountdown();
        self.elExtend.focus();
        this.elExtend.on('click', () => {
            self.restartCounters();
            self.callWebApi();
            (<any>$).modal.close();
            self.attachHandlers();
        });

        this.elExtend.keydown(function (event) {
            if (event.shiftKey && event.which == 9) {
                event.preventDefault();
                self.elDestroy.focus();
            }
        });

        this.elDestroy.keydown(function (event) {
            if (!event.shiftKey && event.which == 9) {
                event.preventDefault();
                self.elExtend.focus();
            }
        });

        this.elDestroy.on('click', InactivityAlert.navigateAway);
        this.attachHandlers();
    }

    attachHandlers() {
        var self = this;
        $(document).on("modal:open", function (event, modal) {
            self.elExtend.focus();
        });
    }
    detachHandlers() {

    }
    destroy() {
        this.detachHandlers();
        this.stopAllCounters();
        this.elExtend.off('click');
        this.elDestroy.off('click');
    }
}

let body = $(document.body);

new InactivityAlert(body.data("timeout-totalseconds") || defaultSessionTimeout, body.data("timeout-messageseconds") || defaultPopupSessionTimeout, body.data("timeout-apiurl") || "");

var secondsToMilliseconds = 1000;
var defaultSessionTimeout = (20 * 60);
var defaultPopupSessionTimeout = (18 * 60);
var InactivityAlert = /** @class */ (function () {
    function InactivityAlert(sessionSeconds, showAfterSeconds, timeoutApiUri) {
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
    InactivityAlert.getTimeoutUrl = function () {
        return $(document.body).data("timeout-url") || "/helper/TimeoutResult";
    };
    InactivityAlert.navigateAway = function () {
        window.location.href = InactivityAlert.getTimeoutUrl();
    };
    InactivityAlert.prototype.setSessionTimeout = function () {
        this.timeoutForSession = window
            .setTimeout(InactivityAlert.navigateAway, this.sessionSeconds * secondsToMilliseconds);
    };
    InactivityAlert.prototype.setTimeoutForModal = function () {
        var _this = this;
        var el = $('#timeout-dialog');
        var self = this;
        console.log("Should trigger after " + this.showAfterSeconds);
        this.setAccessibilityMessage();
        this.timeoutForModal = window
            .setTimeout(function () {
            console.log("Initiating the timeout dialog");
            var count = 1000;
            var startTime = (self.sessionSeconds - self.showAfterSeconds) * secondsToMilliseconds;
            var splitMessage = _this.elMessage.length ? _this.elMessage.html().split(/ [0-9:]+ /) : '';
            _this.detachHandlers();
            var updateMessage = function uM() {
                if (this.elMessage.length) {
                    var formatted = moment.utc(startTime).format('m:ss');
                    this.elMessage.html(splitMessage[0] + " " + formatted + " " + splitMessage[1]);
                }
                startTime -= count;
            }.bind(_this);
            updateMessage();
            _this.intervalToUpdate = window.setInterval(updateMessage, count);
            sessionStorage.setItem("ReturnFocus", document.activeElement.id);
            el.modal({
                escapeClose: false,
                clickClose: false,
                showClose: false
            });
        }, this.showAfterSeconds * secondsToMilliseconds);
    };
    InactivityAlert.prototype.setAccessibilityMessage = function () {
        var splitMessage = this.elAccessibilityMessage.length ? this.elAccessibilityMessage.html().split(/ [0-9:]+ /) : '';
        var totalTime = (this.sessionSeconds - this.showAfterSeconds) * secondsToMilliseconds;
        this.elAccessibilityMessage.html(splitMessage[0] + " " + moment.utc(totalTime).format('m') + " " + splitMessage[1]);
    };
    InactivityAlert.prototype.startCountdown = function () {
        this.setTimeoutForModal();
        this.setSessionTimeout();
    };
    InactivityAlert.prototype.stopAllCounters = function () {
        window.clearTimeout(this.timeoutForModal);
        window.clearTimeout(this.timeoutForSession);
        window.clearInterval(this.intervalToUpdate);
    };
    InactivityAlert.prototype.restartCounters = function () {
        this.stopAllCounters();
        this.startCountdown();
        return true;
    };
    InactivityAlert.prototype.callWebApi = function () {
        if (this.timeoutApiUrl !== null && this.timeoutApiUrl !== "") {
            $.ajax({
                type: "GET",
                url: this.timeoutApiUrl
            }).done(function (result) {
                console.log('User chose to extend session.');
                if (sessionStorage.getItem("ReturnFocus") != null && sessionStorage.getItem("ReturnFocus").length > 0) {
                    document.getElementById(sessionStorage.getItem("ReturnFocus")).focus();
                }
            });
        }
    };
    InactivityAlert.prototype.init = function () {
        var self = this;
        this.startCountdown();
        self.elExtend.focus();
        this.elExtend.on('click', function () {
            self.restartCounters();
            self.callWebApi();
            $.modal.close();
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
    };
    InactivityAlert.prototype.attachHandlers = function () {
        var self = this;
        $(document).on("modal:open", function (event, modal) {
            self.elExtend.focus();
        });
    };
    InactivityAlert.prototype.detachHandlers = function () {
    };
    InactivityAlert.prototype.destroy = function () {
        this.detachHandlers();
        this.stopAllCounters();
        this.elExtend.off('click');
        this.elDestroy.off('click');
    };
    return InactivityAlert;
}());
var body = $(document.body);
new InactivityAlert(body.data("timeout-totalseconds") || defaultSessionTimeout, body.data("timeout-messageseconds") || defaultPopupSessionTimeout, body.data("timeout-apiurl") || "");

# nidirect-dotnet-core-template


| Build  | Branch | Status |
| ------------- | ------------- | ------------- |
| Sonar Cloud  | main  | |

This is not meant to be an exhaustive template but rather a good head start in term of setting up the UI for NI Direct applications with the GDS design pattern using DotNet Core. I have added in some extra ones such as a timeout feature which is useful.

The below links show you how to install the npm packages and set up in case you need to do it yourself, but you don't need to do that for this template as Ive done it for you. When compiling the sass files I recommend using the live Sass compiler from the marketplace in Visual Studio Code.

https://uxg.nidirect.gov.uk/applying_brand.html

https://frontend.design-system.service.gov.uk/#gov-uk-frontend

# How to install this template

Clone the repo

Use command line to navigate to cloned project i.e. cd

To install run:
```
dotnet new -i .\
```

Then run this command to list templates (it should be in there):

```
dotnet new -u
```

Create a new folder somewhere for your project

Then run:

```
dotnet new nidirect-app-frontend -t "NewAppName"
```

Your new web project with all the GDS design and NI Direct uxg should be generated

# Secrets

The app needs some secrets for the govuk pay, stripe and address lookup examples. You can obtain these from govuk pay or stripe.



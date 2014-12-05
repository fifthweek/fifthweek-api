# Fifthweek API

## Development setup

1.  Open the following file:

        ~/Documents/IISExpress/config/applicationhost.config

2.  Find the `sites` element and ensure the following exists as a child:

        <site name="Fifthweek.Api" id="1">
            <application path="/" applicationPool="Clr4IntegratedAppPool">
                <virtualDirectory path="/" physicalPath="\\psf\Home\Documents\fifthweek-api\Fifthweek.Api" />
            </application>
            <bindings>
                <binding protocol="https" bindingInformation="*:44301:10.211.55.3" />
                <binding protocol="http" bindingInformation="*:15376:10.211.55.3" />
            </bindings>
        </site>

    *Note: you will need to update the `physicalPath` to reflect the correct location*

3.  Add the following environmental variables:

        BROWSER_STACK_ACCESS_KEY=<password>
        BROWSER_STACK_USERNAME=<email>

4.  Run Visual Studio 2013 as *admin* to avoid IIS Express related errors.

## Temporary database

Data is stored in `App_Data` and is persisted between builds of the application.

This directory must be manually cleared to reset the database.

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

## Trace Logging on Azure

The trace log can be viewed via the Kudu interface for the relevant project, or using curl.

For example, you can use curl to connect to the fifthweek-api project as follows:

    curl -u [AZURE_ADMIN_USERNAME] https://fifthweek-api.scm.azurewebsites.net/logstream -k

Azure enables tracing when you connect, and disables it after 30 minutes.  You may need to check
the Azure portal to set the logging level you require.


## Check-in procedure

The following must succeed locally before any changes are pushed:

    cd ../fifthweek-web
    git pull
    grunt test:local:dist

### Cross-repository changes

*Extra care must be taken until we have [full continuous integration][full-ci-issue].*

Components must be deployed in their dependency order. You must wait until a dependency has passed through CI / been 
deployed before pushing any dependent components to master. Example:
 
> `fifthweek-web` depends on `fifthweek-api`, 

> Let's assume changes are made to API that are required by Web.

> `fifthweek-api` must be pushed *and become live* before pushing `fifthweek-web` to `master`.

#### Breaking changes

Dependencies must not introduce breaking changes. This means older versions of `fifthweek-web` must work with newer 
versions of `fifthweek-api`. 

This is achieved by introducing changes as additions, whereby existing functionality becomes deprecated instead of being 
removed or overwritten with incompatible modifications. The deprecated functionality can be removed when no live 
components depend on it.

There is no procedure for when you *absolutely must* implement a breaking change. Just remember there's 15 minutes 
between builds on Travis CI!

[full-ci-issue]: https://github.com/fifthweek/fifthweek-web/issues/40 "Issue #40: Full Continuous Integration"

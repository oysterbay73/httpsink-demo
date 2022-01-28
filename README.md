# httpsink-demo

# Introduction

Contains projects to test serilog http sink https://github.com/FantasticFiasco/serilog-sinks-http. Solution originally created to demonstrate
an issue with .net framework web project logging, now resolved. Other successful project types also included for reference.

# About the project

The repo holds the following projects.

## ConsoleApp 

Core (.net 6.0) Console app project. Continues logging message until escape key pressed. Runs and logs successfully with no issues.

## ConsoleNetFw

A legacy .net framework (4.7.2) version of the console app. Runs and logs successfully with no issues.

## WebApp

Core (.net 6.0) web application.
This is a copy of the starter site, generated with the template included in Visual Studio. The only addition is the logging implementation.
Runs and logs successfully with no issues.

## WebNetFw

Legacy .net framework (4.7.2) web application, also generated with the template included in Visual Studio, with logging added. Logger was originally
created in contoller which mimicked injected logger where problem originally identified. Now moved to global and now runs amd logs successfully with no issue.

## Executing and Debugging

* Recommended that Visual studio is used, set start up project as required and debug (for web projects this will run in iis express).
* Note that all logger configurations are hard-coded in the applicable project ```Program.cs```, except the framework web project where it is instead in ```Controllers/HomeController.cs```
* The failing .net framework web project anecdotally appears to fail more quickly then when running in iis over iis express. To run in IIS, bind an IIS site to the site root. To debug attach to the iis worker process.
* No valid http target is provided so all requests to push the logs from the files to the http target will fail unless you provide a valid target (```requestUri```).
* It is not necessary to provide a valid http target to demonstrate the failure in the .net framework web project.
* When running the framework web project, when the original error occurred, this was ```Serilog.Sinks.Http.Private.Network.HttpLogShipper: System.IO.IOException: The process cannot access the file 'buffer.bookmark' because it is being used by another process```.
* Note that without a valid http target other expected errors are logged.



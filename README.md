# ai-sln-test
Repo for the a.i. solutions coding test

Requirements:
- Windows, with .NET 4.7.2 framework

How to run:
- place the following in a directory of your choice - 'config.json', 'AiCustomTaskScheduler.Worker.exe', 'AiCustomTaskScheduler.exe', 'Newtonsoft.Json.dll'
- update 'config.json' task_location to the full path to the 'AiCustomTaskScheduler.Worker.exe'
- use Windows command-line to execute 'AiCustomTaskScheduler.exe' providing the full path to 'config.json' as the only command-line argument

Functionality requirements:
- Tasks can be scheduled or ran immiediately
- Tasks can be canceled/killed before they have completed execution
- Tasks should only execute one instance of the program while it's running
- User configurable time can be a time interval (ie. every 5 minutes), or a specific time (03/16/2021 at 4:00PM EST)
- An example task can be a simple program that simulates work or execution by waiting
- User config should also include the location of the program to run at the scheduled time
- Multiple different configured tasks should be able to run simultanouesly
- User config is a serialized JSON config read from a file, status of running tasks can be output to console
- Task scheduler should accept the following commands:
        start "TaskName" - starts task based on config, which is either immiediately or scheduled
        stop "TaskName" - stops task immiediately
- Use the location that corresponds to your system, ie. Can limit to either Windows (.exe), Linux (anything with execute bit) or Mac

Current state of implementation:
- All tasks configured in the config.json file will be loaded and scheduled/started by the application automatically
- Multiple configured tasks can run simultaneously
- User config business logic checking is limited, especially in the area of task_scheduled datetime string checking
- Unit-test coverage of the main scheduler class not implemented

Requirements not yet met during 3hr time-box implementation:
- Tasks cannot be cancelled/killed before they have completed execution via command-line commands. They can be cancelled by killing the process directly, which won't affect execution on main program thread.
- The checking for if a task is currently in execution seems to be buggy. Therefore (especially duing repeated tasks) more than one of the same task can be executing at the same time
- Command-line commanding of task execution



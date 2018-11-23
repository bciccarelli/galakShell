# GalakShell
[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Galakshell is a shell which allows you to access multiple command files and run them from the commandline. 
  - Can be used to reduce the amount of typing you do
  - Better alternative to batch
  - Modular

# Documentation
#### How it works
GalakShell connects all of your custom commands into a single shell. This means that you can create what will be called **command files.** These are files which house the code of a command. A simple example would be
```
{[gitInit] [p0]
write gitting:[p0]
run /K cd [p0] && git init
}
```
This example would be executed as follows:
```
gitInit [folder] 
```
and would write the name of the folder, open command prompt, run `cd [folder]`, and finally run `git init`

**This example is included** when you download the source code.
#### Getting Started
Data.gshell files will be set up automatically upon the inclusion of a commandFile
**Using:**
```
createCommand [commandLocation] 
```
will include a command file, and allow you to access its commands during this session, and every session thereafter.

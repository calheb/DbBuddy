<div align="center">

<pre>
  ____  _     ____            _     _       
 |  _ \| |__ | __ ) _   _  __| | __| |_   _ 
 | | | | '_ \|  _ \| | | |/ _` |/ _` | | | |
 | |_| | |_) | |_) | |_| | (_| | (_| | |_| |
 |____/|_.__/|____/ \__,_|\__,_|\__,_|\__, |
                                      |___/ 
----------------------------------------------
the easiest way to swap connection strings
</pre>

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

</div>

When working with a development and local database copy, it can be cumbersome to constantly copy and paste new connection strings.
Use a terminal command instead.

## Installation

Install the latest ![release](https://github.com/calheb/DbBuddy/releases/), or clone this repo and build from source.
<p>Currently available on Windows.</p>

## Usage
To view command line arguments

```
dbb
```

### Command-line Arguments
The installer automatically adds DbBuddy to your system's PATH, allowing for simple command-line interaction.

To activate the setup interface
```
dbb start
```
```
  ____  _     ____            _     _
 |  _ \| |__ | __ ) _   _  __| | __| |_   _
 | | | | '_ \|  _ \| | | |/ _` |/ _` | | | |
 | |_| | |_) | |_) | |_| | (_| | (_| | |_| |
 |____/|_.__/|____/ \__,_|\__,_|\__,_|\__, |
                                      |___/

Current Db: Local Db

Main Menu
---------
Select an option:
[1] Local Db
[2] Remote Db
[3] Settings
[4] Exit

```
Once the connection strings have been set, simply toggle back and forth with 

```
dbb set local
```

(or)

```
dbb set remote
```

The Db settings can be set through either the interface with (```dbb start```), or by manually editing the json file located in the installation folder.
(Run the program to generate a file with blank defaults, or create one.

### Sample connectionStrings.json file

```json
{
  "LocalDb": "Data Source=localDbName; Integrated Security=true; Initial Catalog=YourDatabaseName; uid=YourUserName; Password=yourPassword;",
  "LocalDbName": "LocalDbName",
  "RemoteDb": "\"Data Source=remoteDbName; Integrated Security=true; Initial Catalog=YourDatabaseName; uid=YourUserName; Password=yourPassword;\"",
  "RemoteDbName": "RemoteDbName",
  "ConfigPath": "C:\\Users\\Caleb\\source\\repos\\TestWebsite\\TestWebsite\\Web.config",
  "CurrentDb": "Local Db"
}
```

## Meta

Caleb Hebert – caleb.b.hebert@gmail.com

Distributed under the MIT license. See `LICENSE` for more information.

[https://github.com/calheb/](https://github.com/calheb/)

## Contributing
Right now the tool is set up for my (fairly specific) use case, but if you'd like to add a feature, here's how.
1. Fork it (<https://github.com/calheb/DbBuddy/fork>)
2. Create your feature branch (`git checkout -b feature/fooBar`)
3. Commit your changes (`git commit -am 'Add some fooBar'`)
4. Push to the branch (`git push origin feature/fooBar`)
5. Create a new Pull Request

## Disclaimer
Windows Defender will flag the installer due to being an unsigned executable 😭
<p>Code signing cert pending...</p>

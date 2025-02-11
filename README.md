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

When working with a both a remote development and local database copy in ASP.NET, it can be cumbersome to manually update connection strings.
Use a terminal command instead.

## Installation

Install the latest ![release](https://github.com/calheb/DbBuddy/releases/), or clone this repo and build from source.
<p>This tool is availably on Windows.</p>

## Usage
To view command line usage

```
dbb
```

### Command-line Arguments
The installer automatically adds DbBuddy to your system's PATH, allowing for simple command-line interaction.

First, set up your databases in the json configuration file `connectionStrings.json`.

The database settings can be set by manually editing the json file located in the installation folder.
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
Once the connection strings have been set, simply toggle back and forth with 

```
dbb set local
```

(or)

```
dbb set remote
```

## Meta

Caleb Hebert – caleb.b.hebert@gmail.com

Distributed under the MIT license. See `LICENSE` for more information.

[https://github.com/calheb/](https://github.com/calheb/)


## Note
Windows Defender may flag the installer as unrecognized.

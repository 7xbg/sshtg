# sshtg (linux only)

## Usage
SSGTG is a telegram bot that can edit files in your computer with specific commands.
Texts get processed first in *Program.cs* then in *Processor.cs*. Commands are stored in
*CommandHandler.cs*. Each element in the array is a class inhereted from the ICommand 
data type. *Processor.cs* iterates through each command and filters people if they're
not whitelisted, or if the command is a dev command, if they're a dev. The whitelist is
stored in *whitelist* and devs are stored in *devs*. To disable the whitelist go to *Data.cs*
and make **useWhietlist** *false*. To disbale dev whitelist make **useDevWhitelist** *false*
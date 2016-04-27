# InputMapper
(yet another) input mapper for Unity :)

## Features
- three types of devices: digital keyboard, analog keyboard with customizable response, Xbox 360 / Xbox One controllers (through XInputDotNet)
- two types of commonly used commands: axis command (e.g. strafe),  command (e.g. jump)
- fine granularity in mapping your devices's buttons/axes/keys for axis commands: mapping *left stick -X* to a negative side does not imply that *left stick +X* will be mapped to the positive side
- customizable GUI for interactively mapping device buttons to commands
- serialization of your configuration from/to XML

## Notes
- due to changes in Unity's scene management, you might have to adjust a few things to your needs
- this is a free and open source project, feel free to fork, improve and PR !

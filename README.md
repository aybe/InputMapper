# InputMapper
An input mapper for Unity : maps devices to commands, with a GUI, serialization and analog keyboard emulation.

## Features
- provides a way to map keyboard and joystick buttons/axes/triggers to your game's commands
- two types of commonly used commands: axis command (e.g. strafe), button command (e.g. jump)
- axis command buttons are indepedent: e.g. mapping left stick -X on the left side does not imply that left stick +X will be on the right side (i.e. complete freedom in your layout)
- three types of devices: digital keyboard, analog keyboard with customizable response, Xbox 360/One Controller (through XInputDotNet)
- an input configuration that holds your commands and is serializable from/to XML
- a customizable GUI for interactively mapping device buttons to commands

## Notes
- to run the demo scene `InputMapper/InputWizardSample.unity` you need to have `InputMapper/InputWizard.unity` added to your Build Settings, **it hasn't been included for your convenience**
- the GUI uses a *prefab button* to display each command button that is to be mapped, a default one has been included but feel free to provide your own
- the GUI default layout is a stretched scroll viewer, adjust it to your needs
- currently there is no mechanism to go *back* to a previous scene once a user is finished mapping its commands, TODO

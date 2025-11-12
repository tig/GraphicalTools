# Terminal.Gui v2 Migration Status

## Overview
This document tracks the progress of migrating Out-ConsoleGridView (OCGV) from Terminal.Gui v1.14.0 to v2.0.0.

## ✅ Migration Complete!

The code now successfully compiles with Terminal.Gui v2.0.0 on .NET 8.0. All major API changes have been implemented.

## Completed Changes

### Project Configuration
- ✅ Updated `global.json` from .NET 6.0 to .NET 8.0 with rollForward policy
- ✅ Updated all `.csproj` files to target `net8.0`
- ✅ Updated `GraphicalTools.build.ps1` to use net8.0
- ✅ Updated Terminal.Gui package reference from 1.14.0 to 2.0.0
- ✅ Enabled nullable reference types in all projects
- ✅ Enabled latest C# language version

### Code Modernization
- ✅ Converted to file-scoped namespaces
- ✅ Added nullable reference type annotations
- ✅ Updated event handler signatures

### Terminal.Gui v2 API Changes Implemented

#### Application Initialization
- ✅ Replaced `Application.UseSystemConsole` with `Application.ForceDriver = "NetDriver"`
- ✅ Updated `Application.Init()` calls (no longer takes driver parameter)

#### StatusBar
- ✅ Replaced `StatusItem` with `Shortcut` throughout
- ✅ Updated StatusBar constructor to accept `IEnumerable<Shortcut>` instead of `StatusItem[]`

#### IListDataSource
- ✅ Updated `Render()` method signature:
  - Old: `void Render(ListView, ConsoleDriver, bool, int, int, int, int, int)`
  - New: `void Render(ListView, bool, int, int, int, int, int)`
- ✅ Added `SuspendCollectionChangedEvent` property
- ✅ Added `CollectionChanged` event
- ✅ Updated `Length` property implementation
- ✅ Implemented `IDisposable`

#### View Changes
- ✅ Updated Window constructor to use initializers instead of parameters
- ✅ Changed `win.Border.BorderStyle` to `win.BorderStyle`
- ✅ Updated to use `LineStyle.None` instead of `BorderStyle.None`
- ✅ Removed `Terminal.Gui.Trees` namespace (TreeView is now in main namespace)
- ✅ Label constructor now uses Text property initializer
- ✅ TextField constructor now uses Text property initializer
- ✅ ListView constructor uses Source property initializer

#### Method Renames
- ✅ `SetNeedsDisplay()` → `SetNeedsDraw()`
- ✅ `Key.Null` → `Key.Empty`
- ✅ `Redraw(Bounds)` → `SetNeedsDraw()`

#### Key Bindings
- ✅ `Key.CtrlMask` → `Key.WithCtrl` extension method
- ✅ `ClearKeybinding()` → `KeyBindings.Remove()`
- ✅ Removed `AddKeyBinding()` calls (ListView handles Space by default)

#### Colors API
- ✅ `Colors.Base` → Direct ColorScheme usage
- ✅ `Colors.Error` → ColorScheme with Attribute(Color.BrightRed, Color.Black)
- ✅ Updated ColorScheme API usage throughout

#### ListView Changes
- ✅ `MarkUnmarkRow()` → Manual mark toggling logic for Single mode

#### Driver API
- ✅ `driver.AddRune(uint)` → `driver.AddRune(System.Text.Rune)`
- ✅ Rune width calculations updated for display

#### Event Handlers
- ✅ `TextChanged` event signature updated to `EventHandler<EventArgs>`
- ✅ Updated all event handler signatures to include sender parameter

### Build Status

✅ **Build Succeeds** - The project compiles successfully with only nullable reference warnings (non-blocking).

## Remaining Work

### Testing
- [ ] Manual testing of Out-ConsoleGridView with various data types
- [ ] Test filtering functionality
- [ ] Test Single/Multiple/None output modes
- [ ] Test Show-ObjectTree functionality
- [ ] Test MinUI mode
- [ ] Verify command-line backwards compatibility
- [ ] Test NetDriver vs default driver

### Unit Tests
- [ ] Create xUnit test project
- [ ] Add tests for command-line parameter parsing
- [ ] Add tests for data model classes
- [ ] Add tests for filtering functionality
- [ ] Add integration tests for UI workflows

### Documentation
- [ ] Update README with new requirements (.NET 8.0, Terminal.Gui v2)
- [ ] Update build instructions
- [ ] Document any breaking changes in command-line behavior
- [ ] Add migration notes for users

### Optional Improvements
- [ ] Address nullable reference warnings (non-critical)
- [ ] Investigate TableView as alternative to ListView
- [ ] Performance testing and optimization

## Terminal.Gui v2 API References Used

- [Official Migration Guide](https://gui-cs.github.io/Terminal.Gui/docs/migratingfromv1)
- [v2 API Documentation](https://gui-cs.github.io/Terminal.Gui/api/)
- [Breaking Changes Thread](https://github.com/gui-cs/Terminal.Gui/discussions/2448)
- [IListDataSource Interface](https://gui-cs.github.io/Terminal.Gui/api/Terminal.Gui.Views.IListDataSource.html)
- [StatusBar/Shortcut Changes](https://gui-cs.github.io/Terminal.Gui/api/Terminal.Gui.Views.StatusBar.html)
- [ColorScheme Documentation](https://gui-cs.github.io/Terminal.Gui/docs/scheme.html)

## Next Steps

1. ✅ ~~Fix all compilation errors~~ **COMPLETE**
2. Perform manual testing of all features
3. Add comprehensive unit tests
4. Verify backwards compatibility
5. Update documentation
6. Release updated module

## Notes

- The migration required extensive API changes due to Terminal.Gui v2's architectural improvements
- Command-line interface remains the same for backwards compatibility
- The ListView-based UI is maintained; TableView can be explored in future enhancements
- All modern C# and .NET constructs are now in use (.NET 8.0, nullable reference types, file-scoped namespaces)

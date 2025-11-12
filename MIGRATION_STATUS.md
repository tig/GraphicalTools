# Terminal.Gui v2 Migration Status

## Overview
This document tracks the progress of migrating Out-ConsoleGridView (OCGV) from Terminal.Gui v1.14.0 to v2.0.0.

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

#### Method Renames
- ✅ `SetNeedsDisplay()` → `SetNeedsDraw()`
- ✅ `Key.Null` → `Key.Empty`

## Remaining Work

### Critical API Changes Still Needed

1. **Constructor Changes**
   - Label constructor no longer accepts string parameter
   - TextField constructor no longer accepts string parameter
   - Need to use initializers for Text property

2. **Key Binding Updates**
   - `Key.CtrlMask` → `Key.WithCtrl` extension method
   - `ClearKeybinding()` API has changed
   - `AddKeyBinding()` signature may have changed

3. **ListView Changes**
   - `MarkUnmarkRow()` method name/behavior changed
   - Need to verify selection API

4. **Colors API**
   - `Colors.Base` → needs update (ColorScheme changes)
   - `Colors.Error` → needs update
   - ColorScheme API has changed in v2

5. **View Methods**
   - `Redraw(Bounds)` → API changed
   - `Bounds` property access may have changed

6. **Driver API**
   - `driver.AddRune(uint)` → needs `Rune` type instead of uint
   - May need to use different Rune creation API

### Models Project Warnings
Need to fix nullable reference warnings in:
- `ApplicationData.cs` - properties need `required` modifier or nullable types
- `DataTableColumn.cs` - nullability issues
- `DataTableRow.cs` - nullability issues
- `Serializers.cs` - possible null reference returns

### Testing
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

## Terminal.Gui v2 API References Used

- [Official Migration Guide](https://gui-cs.github.io/Terminal.Gui/docs/migratingfromv1)
- [v2 API Documentation](https://gui-cs.github.io/Terminal.Gui/api/)
- [Breaking Changes Thread](https://github.com/gui-cs/Terminal.Gui/discussions/2448)
- [IListDataSource Interface](https://gui-cs.github.io/Terminal.Gui/api/Terminal.Gui.Views.IListDataSource.html)
- [StatusBar/Shortcut Changes](https://gui-cs.github.io/Terminal.Gui/api/Terminal.Gui.Views.StatusBar.html)

## Build Status

Current build has compilation errors that need to be addressed. See above "Remaining Work" section for details.

## Next Steps

1. Fix all remaining constructor calls (Label, TextField)
2. Update Colors/ColorScheme usage
3. Fix key binding API calls
4. Update Rune/AddRune usage in GridViewDataSource
5. Fix remaining method calls (MarkUnmarkRow, Redraw, etc.)
6. Address all nullable warnings in Models project
7. Add comprehensive tests
8. Verify backwards compatibility of command-line parameters
9. Manual testing of all features
10. Update documentation

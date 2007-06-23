Regulazy by Roy Osherove
---------------------------------
Blog: ISerializable.com
Email: Roy@Osherove.Com (Not for bug reports! - see below for those)
---------------------------------

This program is provided free for use, AS-IS.
You may send bug reports, feature requests and anything else which relates to this application
by email to Support@ISerializable.com or by going to http://bugz.osherove.com 

What's new
____________
Version 1.03 - 9/10/06
- Fixed Some bugs relating to creating encapsulating scopes from existing ones
- Fixed exception when trying to set existing scope as new scope
- Fixed tooltip will now show only first 100 chars of rule if rule is too long
- Added: Automatic Scope creation Button for known patterns in Text. When you load a piece of text for the first time
		Regulazy will automatically highlight parts of the text it can recognize as common patterns
		based on patterns in the file Auto.Rules in the application directory. This should save plenty of time for initial work.
		Automatic scopes are performed on a background thread.
- Added many rules to the auto.rules file
- Enhnaced speed of drawing symbols on screen and other features
- Added: Suggestion process is run on a background thread.
- Added: Import sample from a web page Dialog: Menu Import-Web Page
	-(works only some times right now): Select rendred page text and get the underlying HTML
	- or select parts of the actual Page HTML (always works)
- Added: Moving between Regex and Text Edit mode without changing the text - text scopes are remembered

Version 1.02 10/8/06
- Added ability to rename the parent scope of the selected scope (A parent scope is always filled up with child scopes so it's impossible to select it by itself)
- Added highlighting of text parts when hovering on contect menu item, so you can see what text your action will take place on
- Added ability to create a large scope out of multiple selected smaller text scopes and rename it
- Fixed Spelling of "Multiline" in regex options
- Added more date recognition patterns to custom.rules

Version 1.012 10/8/06
- Fixed bug: Rename Scope when selected text is not empty renames wrong scope
- Fixed: Selection helpers stopped working in the last versionl Now they work again.
	These automatically select upto the boundary of a word on double click, and similar charg on ctrl-Click.

Version 1.011 10/8/06
- Fixed error with expression optimizer working on some inputs

Version 1.01 10/8/06

 - Added support for external .rules files (see "how to edit rules.txt")
 - fixed minor bugs
 - Fixed Bug in VB Sample Generation template for group name indexers.
 - Added support for running in windows Vista
 - Fixed Error when Regex Sample Text was empty
 - Fixed painting of spaces and newlines that continued even when not in "manipulation mode"
 - Added ability for Regulazy to automatically try and decide if resulting Expression will contain the "line start" and "line end" marks 
	based on the amount of matches that each returns. It will favor the one returning the most matches but will only work if the default (with) marks return 1 or less matches.
	

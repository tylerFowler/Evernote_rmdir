Evernote rmdir
============================
Are you like me and have a steady pile of growing notes that were once reminders? Unfortunately, Evernote doesn't offer an option to delete all reminders in your account that are marked as completed, and so now I have about a dozen useless notes in my personal notebook marked "go grocery shopping" or "study for that exam tomorrow!"

This kind of thing causes me grief, so I thought I'd write a simple .NET Windows application that deletes these unwanted reminders. Evernote rmdir will go through your account, per your criteria (searching certain notebooks, excluding reminders with a certain tag, or excluding the reminders that were marked as completed within a certain number of days) and move these notes to the trash. 

Protip: because of the way this tool queries your Evernote account, when you set what tag to exclude, you can use a bit of Evernote's searching lingo. For example if you type in "a*" then it will exclude every reminder with a tag that starts with the letter a. (For a more complete list of searching terms: http://dev.evernote.com/doc/articles/search_grammar.php)

####Feature List (so far)
- Only delete reminders that have been marked as completed for a certain number of days
- List view to select the notebooks to include in the deletion
- Exclude reminders with a particular tag
- 

###To Do:
- Test the query to the Evernote API (more thoroughly)
- Implement OAuth (just uses the developer sandbox right now)

### License
The MIT License (MIT)

Copyright (c) 2013 Tyler Fowler - tylerfowler.1337@gmail.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

Evernote rmdir
============================
Are you like me and have a steady pile of growing notes that were once reminders? Unfortunately, Evernote doesn't offer an option to delete all reminders in your account that are marked as completed, and so now I have about a dozen useless notes in my personal notebook marked "go grocery shopping" or "study for that exam tomorrow!"

This kind of thing causes me grief, so I thought I'd write a simple .NET Windows application that deletes these unwanted reminders. Evernote rmdir will go through your account, per your criteria (searching certain notebooks, excluding reminders with a certain tag, or excluding the reminders that were marked as completed within a certain number of days) and move these notes to the trash. 

Protip: because of the way this tool queries your Evernote account, when you set what tag to exclude, you can use a bit of Evernote's searching lingo. For example if you type in "a*" then it will exclude every reminder with a tag that starts with the letter a. (For a more complete list of searching terms: http://dev.evernote.com/doc/articles/search_grammar.php)

Protip Pt. 2: if you're a bit on the edge about running a tool that deletes notes out of your Evernote account, then you and I have something in common. The application uses search syntax to determine which notes to delete based on your criteria (as stated above). So if  you set the tool to delete all completed reminders, you can go to Evernote and run a search for ```reminderDoneTime:*``` and it will show you all the notes that Evernote rmdir would delete in that pass.

####Feature List (so far)
- Only delete reminders that have been marked as completed for a certain number of days
- List view to select the notebooks to include in the deletion
- Exclude reminders with a particular tag

####To Do:
- Test the query to the Evernote API (more thoroughly)
- Implement OAuth (just uses the developer sandbox right now)
- Add an undo button

###How to Use:
1. Open the Evernote rmdir.sln file
2. In EvernoteDevAuth.cs change the value of ```authToken``` on line 30 to your developer token
3. If you are using a Sandbox account, then make sure line 96 is uncommented and line 99 is commented out (in sandbox, you're NoteStore URL is generated by a method call)
   - If you are wanting to use a personal account, go to Evernote's website and get your Developer Token and NoteStore URL (for more information go [here](http://dev.evernote.com/doc/articles/authentication.php#devtoken)) and set ```noteStoreUrl``` on line 99 to the one given to you on the Evernote site. The Developer Token goes in the same place as if you were using a Sandbox account.
4. Now run Build -> Rebuild Solution (in Visual Studio), then you should be able to find the binary in ```Evernote_rmdir/Evernote rmdir/bin/Release```

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

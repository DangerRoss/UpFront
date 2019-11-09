UpFront.Net

An small utility library I made a while ago for .NET Framework updated to .NET Standard 2.0. 
Purpose of UpFront was to help lower heap allocations and overall GC overhead through better control of reusing 
memory for intermediate aged objects and using up front allocated heap space for critical paths where stack space is too restrictive.

While I still use UpFront, parts of it are redundant due to tools avaialable in System.Buffers and various features in .NET Core. 


UpFront.Buffers
Buffers provides a container type with a seekable write cursor over a fixed or growable block of memory and has direct access for copying. The UpFront ArrayBuffer<T> effectively acheives the same role as ArrayBufferWriter<T> from System.Buffers.


UpFront.MutableString
MutableString is a Upfront ArrayBuffer with added features to fulfill the role of a StringBuilder.
Unlike StringBuilder, MutatbleString doesn't use any heap allocation to write primitives but at the loss of format control.


UpFront.Pooling
Pooling provides a object pool for allowing reuse of objects. The events of a pooled object being initialised, reused and dropped by a pool can be subscribed to using attributes on member functions of the class to assure safe state control.


UpFront.Events
Events provides a couple of simple Observables that are intended to be used in place of a .NET event handler when subscribing/unsubscribing to an event are on a critical path.


Known Issues
Writing a floating value to a MutableString does not correctly handle values requiring scientific notation to be represented in unicode.




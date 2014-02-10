StrangeAttractor.Util.Functional
================================

C# library for error handling in functional style (inspired by Scala and Haskell) - Option (Maybe), Try.
This helps preserve referential transparency when working with operations that might fail (result is unavailable, or they throw an exception).
Results of these operations can be seamlessly used in LINQ queries.

# ** Inspiration **
This implementation was inspired by the Scala and Haskell languages and by an article about Maybe monad http://lanshiva.blogspot.co.at/2011/02/applied-functional-programming-in-c.html

# ** Usage **
** Option ** - Basically a *Maybe* monad, i.e. a container for a value or nothing. Very similar to Nullable<T>.
  >

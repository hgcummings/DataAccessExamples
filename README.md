# DataAccessExamples

Various examples of implementing and testing data access code in .NET - presented as a Web Application (using Nancy).
Part of a talk originally delivered at SDD Conf 2015. Slides available here: https://slides.com/hgcummings/taming-the-database/

## Prerequisites
.NET 4.5
NuGet

## Getting started
* Checkout the code
* Restore NuGet packages
* Run the tests

To run the website, you will also need the copy of the database from [Dropbox](https://www.dropbox.com/sh/ujuxz830ders3tc/AABZ4kVgfLIAsQEqvIazRPtYa?dl=0])
(for now, waiting for [GitHub support for Large File Storage](https://github.com/blog/1986-announcing-git-large-file-storage-lfs) to arrive...).
The connection string in Web.Config will look for this DB on a local instance named SQLSERVER2014.

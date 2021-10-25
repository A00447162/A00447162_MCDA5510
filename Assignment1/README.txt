The goal of the assignment is to count the number of valid rows and skipped rows in all csv files present in a directory.
Given are 3 cs files namely DirWalker, Exceptions and SimpleCSVParser.
DirWalker is used to walk through all the files and directories of a path and get the csv files.
Exceptions is used to know all the catched exceptions.
SimpleCSVParser is used to parse through every row in each csv file encountered and check if all the values asked are empty or not.
If any of the values in the row is empty, it should be counted as skipped row and logged. If none of the columns in the row is empty, then we need to count that as valid row and logged it into output.csv file
For logging purpose, log4net has been used from NugetPackageManager. log4net.config file has been updated with the logging requirement.
Output.csv is present in the Output folder where it contains all the valid rows and total execution time, number of valid rows and number of skipped rows.
Error.log contains what data is missed in which row in which file.

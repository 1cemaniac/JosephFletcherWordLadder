#Blue Prism Word Ladder Read Me

##Planning

To start tackling the problem I decided to split it down into sections. Not only would this make finding a solution easier but it would also allow em to make classes with a single responsibility. 
These sections were:
-Load the words into a data structure
-Creating the ladder
	-Selecting a linkable word
-Outputing the answer

The other important aspect to consider was the data structure I wanted to use. I decided to load the words into HashSets as this would be the fastest data type to search whenever I needed to look for a word. 
For the ladder tracking I chose to use Lists of custom objects as this allowed me to store all the information about the word I would need to decide how to search it. Also the list aspect allowed me to keep them ordered so I could select which items I wanted to search first.

To create the ladder I decided there were 3 possible steps I could take from any word. They were:
-(Forward Step) Match 1 more character e.g. care => card where the goal word is ward
-(Side Step) Match the same character e.g. care => dare where the goal word is ward
-(Back Step) Match 1 less e.g. care => core where the goal word is ward

From this I came up with a 4 step process:
1. Take as many Forward Steps with unchecked words as possible
2. Take a Side Step on all words which have been Forward Checked and then Forward Check the results
3. Take a Back Step on all words which have been Forward and Side Checked and then Forward Check the results
4. Repeat until the goal word is hit with a Forward Step

This meant that every time I repeated a rotaion all the words would fall down the categories until I had check all possible routes from them. It also meant that I was always checking the same number of 'out of the way steps' at any given time. A side step can be thought of as 1 'out of the way step' as it is not getting closer to the goal word. A back step can be thought of as 2 'out of the way steps'as it is actively moving away from the goal word.

The important thing was that every time I found I word I would remove it from the list and link it to the word which it stepped from. This meant that when I found the goal word I could just continue back down the chain of words via the shortest route to create an output.

##Testing

As I decided to use Test Driven Development(TDD) for my solution I created unit tests for all the classes to test for their core functionality. This involved using Moq and Nunit to setup a bank of tests that isolated all the components I was planning to create. All the logic classes would be implemented via interfaces to allow this.

Tests Included:
-Checking the data loaded correctly
-Checking the ladder generateed and returned correctly
-Checking the steps returned the correct result
-Checking that the final outputs were correct
-Checking that the data saved correctly
-Checking it ran start to end as expected

I didn't want to add tests in for every possible senario as I felt that it was more than was required for this project.

##Creating the code 

As all of my logic classes were to stem from interfaces I decided to use AutoFac and implement Dependacy Injection. This would allow me to easily put the classes where they needed to be and let them be switched out in the future if required.
The solution made a bare bones implementation that used a directly brute force version of the method to make sure that it passed the tests. From there I refined it thinking more about how to order the searches and how I could better organise the code.

When creating the logic I wanted to make sure that all of the sections would be scalable if larger or smaller words were required. I also wante to attempt to make them as efficient as possible to keep the performance up for if a larger dictionary was used. As such, I decided to add a simple Machine Learning process that would prioritse the words to search based on how often the letters inside of it appeared in previous operations.

The Priority Tracker idea started by creating more tests to:
-Load a data set
-Log a data set
-Prioritise a set of WordNodes
-Save the results

From there I added it into the soultion making the data used based on the end word size. Any time a word was found to link to another word it would be log a count for every letter that appeared in it.

##Retrospect

The WordProcessing class required a lot of setup to test as the results were coupled to the Ladder Generation class. There is probably a better way to implement this.
I also think that the DataLoader and ResultOutputter could have made use of a factory design pattern to make them more reusable however it would likely have been more complicated than necessary.

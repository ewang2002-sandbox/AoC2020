# Reflections
Here, I'll reflect on the problem that I did. I'll provide my thought process, along with some basic stats. 

### [Day 1](https://adventofcode.com/2020/day/1)
Part A and B were basically making use of two and three for-loops, respectively. However, I could definitely improve on its efficiency (Part A's time complexity is O(n^2) and Part B's time complexity is O(n^3)). 

| Part  | Time Taken |
| ------------- | ------------- | 
| A | ~2 Minutes. | 
| B | ~2 Minutes. | 

### [Day 2](https://adventofcode.com/2020/day/2)
This question was quite interesting. As soon as I saw the inputs, I began devising a way to parse the inputs. A lot of people decided to use RegExp; however, I don't really know how to use RegExp so I chose not to.

Consider the line shown below. All lines in the input will follow this pattern (min-max letter: password)
```
6-7 w: wwhmzwtwwk
```

First, I began by splitting this line by a space (" "). This would leave me with an array:
```
{ "6-7", "w:", "wwhmzwtwwk" }
```

Then, I split the first element by a hyphen so I could get the minimum and maximum amount of times the letter could be in the password. 
```
{ "6", "7" }
```

Alternatively, I could have used C#'s range functionality to get the appropriate character from the first element.
```cs
var data = line.Split(" ");
var min = int.Parse(data[0][0]);
var max = int.Parse(data[0][^1]); 
```

I got the appropriate letter from the second element by replacing the colon with an empty string. Again, I could have used C#'s range functionality. From there, the problem was basically making sure the password was valid by checking whether min <= amount of specified letter in the password <= max. 

The second part was mostly similar. However, the one thing that got me was the indices starting at 1 instead of 0. After spending a few minutes trying to figure out what I did wrong (reading is hard), I managed to solve the problem by checking whether the letter existed in the (first specified index - 1) position of the password OR the (last specified index - 1) position (but NOT both positions). In other words, password[min - 1] ^ password[max - 1]. 

| Part  | Time Taken |
| ------------- | ------------- | 
| A | ~6 Minutes. | 
| B | ~6 Minutes. | 

### [Day 3](https://adventofcode.com/2020/day/3)
Part (A) threw me off a bit. When I saw the input, I thought I had to transpose the given input. This took some valuable time and, sadly, did not work out in my favor.

Once I reread the question, I realized that I was supposed to essentially "copy" the input multiple times until my toboggan could reach the bottom of the given input. At first, I thought about expanding out my input a few hundred times; however, I chose to edit the element only if I had to. This worked out well in my favor. 

I did hit a few exceptions (mainly ArrayIndexOfBoundsException), but that was due to my confusing "right" with "down."

Part (B) was Part (A) but repeated several times. I decided to make an array of all directions I would have to evaluate, and then evaluate the number of trees I would encounter based on the said direction. From there, I could multiply the outputs of each evaluation. 

In my revised code, I accounted for the fact that there was a lot of repeating code. Thus, I decided to create a method that would calculate the number of trees based on the given directions. I also decided to go a more functional route, which was far more concise.  


| Part  | Time Taken |
| ------------- | ------------- | 
| A | ~15 Minutes. | 
| B | ~4 Minutes. | 
# What we know
- We need to grab all words of specified length and put them into data structure

```
public search(letter, index) {
  //for every word in the bank
    //check to see if word.indexOf(index) != letter
      //remove word from bank
  //if the bank is only one word, guess the word
  
}
```

```
public guess() {
  //get new bank of words
  //choose word inside new bank and grab an open letter
  //search with new character
}
```

```
search(key, keyExists, index)
  if keyExists is false
    remove words from bank which have key
  else
    remove words from bank which do not have key.indexOf(index)
    
   ...
 indexes = [0,2]
 for i = 0 to index.length
   search(key, keyExists, indexes[i])
 
 findWord(key, keyExists, indexes);
```

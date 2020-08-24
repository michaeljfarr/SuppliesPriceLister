# Musings of mike
Hi, I thought I'd keep some notes here.

## Timing
1. Start reading: 3:30
2. Create github repo
3. Used https://json2csharp.com/ to convert for Megacorp
4. Created Models for the solution
5. Create unit test project
6. Created first cut, Humpries implementation of supplypriceitemprovider
7. Created first integration test
8. First checkin
9. eep, tests fail.

### Structure
I wasn't really sure what directory structure would make the most sense here, there are lots of patterns that I coud have relied on.  
I seperated the classes between Interfaces and Services because I knew I thought SOLID design principles would be the thing you were searching for.

### External libraries
I considered finding an external CSV reader because that would have better inbuilt error handling than what I can produce easily.  However, 
I decided that this sort of thing would be quite critical to buildxact, so I figured in this situation a bespoke reader will end up being more
extensible and allow us to achieve better long term maintainability.  
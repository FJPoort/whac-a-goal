# whac-a-mole
Created by Frans Jan Poort as an assessment for Miniclip NL.

## Game Rules
For every mole you hit you get points. The amount of points is to be determined by a "designer" via editor variables. Its default value is 1. Also, there is a bonus point system. For every 7 consecutively hit moles, you get 1 bonus point. The amount of '7' is also an editor variable.

Every 15 hits, an extra mole is being activated. And as you may guess, this is also tweakable via an editor variable :).

For every missed mole, a certain amount of seconds will be subtracted from your timer. And yes! The amount of seconds of the time penalty is configurable via the editor.

## Choices made in development
In the GameManager class you will find editor variables to play around with and find the perfect gaming experience.

In the EndScreen scene, I have setup a quick window to show. I went for this quick and dirty approach based on the time available for this assessment. Normally I would use the existing popup system of a company, or build a new one if it doesn't exist yet.

The highscore table in the EndScreen, is a quick and simple setup of a scrollable view. Whenever such a view would display many items, I would choose to make a recyclable view with object pooling. Based on my time available for the assessment, and because performance will most probably not be an issue this project, I chose to skip object pooling for now.

When the application is closed nothing happens unless you are in the EndScreen. If you close the app in the EndScreen in such way that Unity's Event Function "OnApplicationClosed" is called and if the score was not saved, I add the score to the highscore list. I realise while writing this README, that it would have been nice to also show the confirmation popup here and let the player choose to cancel quitting the app and let them input their name. Let's just say that this is an agile approach and we'll implement it once too many players start complaining :)

## Borrowed systems
With the little time available for the assessment, I chose to borrow the save/load system from [this video](https://www.youtube.com/watch?v=aUi9aijvpgs). This shows my ability to adapt to and work with existing systems. Why didn't I write such a system myself? Well, it would be too expensive to reinvent the wheel, not? ;)

## Questions?
Please feel free to contact me whenever there are any questions. I am happy discuss my choices with you.

## Thank you
Thank you for taking the time to assess my project.
-> start

=== start ===
A long time ago, acouple of orcs stole my candy.
It would be nice if I could get it back.
By chance, hace you come across my candy?
    * [Yes.]
    -> success
    * [No.]
    -> noCandy
-> END


=== noCandy ===
Come back if you find my candy.
-> END


=== success ===
Thank you so much! Here's a reqward.
-> END


=== failure ===
Looks like you don't have it. Come back if you find my candy.
-> END


=== postCompletion ===
Thanks for helping me earlier. Good luck with your adventure!
-> END
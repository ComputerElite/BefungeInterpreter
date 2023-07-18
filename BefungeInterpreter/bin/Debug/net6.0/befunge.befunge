>  v
     
    
     
    
       
    
    
    Set up x, y, dx and dy variables
    x: 0, 1 with value 0
    y: 0, 2 with value 0
    dx: 0, 3 with value 0
    dy: 0, 4 with value 0
   > 001p 002p 003p 004p v
     x    y    dx   dy    
  >v                     <
     Read user input and change dx or dy accordingly

              w pressed, pop user input     set dx to 0 and dy to -1
   >~  v      >          $                      003p        01-04p                        v
       >:"w"-!|     
                     a pressed, pop user input     set dx to -1 and dy to 0
                     >          $                      01-03p         004p                v
              >:"a"-!|          
                            s pressed, pop user input     set dx to 0 and dy to 1
                            >          $                      003p        104p            v
                     >:"s"-!|          
                                   d pressed, pop user input     set dx to 1 and dy to 0
                                   >          $                      103p        004p     v
                            >:"d"-!|
                                   
                                                esc pressed exit
                                          >"gniyalp rof sknahT",,,,,,,,,,,,,,,,,,@
                                   >:93*-!|
                                          >    v





   ^        ,,,,,,,,,,,,,,,,"Use wasd to move" <
                  no correct input

   v                                                                                      <




    increment x with dx
    load dx, duplicate, store dx, load x, add, store x
   >     03g :          03p       01g     +    01p v
   v                                               <
    increment y with dy
    load dy, duplicate, store dy, load y, add, store y
   >     04g :          04p       02g     +    02p v
   v                                               <
   > ":x",,01g:01p. ":y",,02g:02p. ":xd",,,03g:03p. ":yd",,,04g:04p. 25*,v
   v                                                                     <
   
   
   
   
    Display player pos

    
    tmp x: 0,5
    tmp y: 0,6
    i: 0,7
    j: 0,8
    
    store tmpx with 0, set i to 30, set j to 30
   > 006p                            325**08pv
   v                                         <
                                                                                 for loop still going (y)
                                                                                 >  06g 1+ 06pv
 > >                              08g                1     -           :08p      |              
  ^                                                                              <                
                                                                                                 
   v                  p70**523             p500                              ,*25             <
    for loop, 9 iterations        push 1 on stack, load i, subtract 1, store i     loop is going, add 1 to tmp x     load y and x onto stack, load tmp x >           >" ",v
                                                                                 >                05g 1+ 05p       > 02g:02p 01g:01p          05g:05p   -|
   >                              07g                1     -           :07p      | i is 0, loop done                                                     > 06g:06p  -|
                                                                                 > v                                                                                 >"X",v
   ^                                                                                                                                                                      <
 ^                                                                                 <
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
                                   >           v
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
    
    load x and y, check if both are 0
                       >  >" ",v
   > 01g:01p 02g:02p 0-|
                       >0-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 0-|
                       >1-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 0-|
                       >2-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 0-|
                       >3-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 0-|
                       >4-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 0-|
                       >5-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 0-|
                       >6-|
                          >"X",v
   v                           <
   5
   2
   *
   ,
                       >  >" ",v
   > 01g:01p 02g:02p 1-|
                       >0-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 1-|
                       >1-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 1-|
                       >2-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 1-|
                       >3-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 1-|
                       >4-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 1-|
                       >5-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 1-|
                       >6-|
                          >"X",v
   v                           <
   5
   2
   *
   ,
                       >  >" ",v
   > 01g:01p 02g:02p 2-|
                       >0-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 2-|
                       >1-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 2-|
                       >2-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 2-|
                       >3-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 2-|
                       >4-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 2-|
                       >5-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 2-|
                       >6-|
                          >"X",v
   v                           <
   5
   2
   *
   ,
                       >  >" ",v
   > 01g:01p 02g:02p 3-|
                       >0-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 3-|
                       >1-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 3-|
                       >2-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 3-|
                       >3-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 3-|
                       >4-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 3-|
                       >5-|
                          >"X",v
   v                           <
                       >  >" ",v
   > 01g:01p 02g:02p 3-|
                       >6-|
                          >"X",v
   v                           <
   5
   2
   *
   ,
  ^<
# Gamer
Exemplar for volitility-based decomposition architecture using several board games.

All are written in .Net 5.  

Single Project Version: The most stripped down version of the VBD pattern.  No interfaces.  
Multi-Project Version: A most feature rich version with the target project layout with interface and service projects for each component: client; manager; engine; and accessor projects as per "The Method." 
Messaging Version: Extends the MPV to wrap all calls between components in messages.  Service messaging allows for corelation id tracking from the first call to the last.  Error handing support is also included.  

I'm working on a CoreWCF version next.  

For more information on this pattern, check out idesign.net and/or "Righting Software" by Juval Lowy.  

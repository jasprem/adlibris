The whole design and the solution has been worked on over few days sporadically. 
The effective time take would be 6 - 8 hours. 

The database solution is under databases folder. In order to run the solution the database needs to be deployed to your local sql server. 
If you wish to test the solution, start the project Catalog.WebApi and navigate to /swagger to see the API endpoints.

I have tried to split the solution into three sub domains - Catalog, Orders and Payment.
Some of them are just stub projects. I have mainly concentrated on completing the code for ordering and checking the product status. 

As seen in the design diagram, the intention is to make most of the parts asychronous by introducing message queues. But the implementation that I have done
is synchronous for simplicity reasons.


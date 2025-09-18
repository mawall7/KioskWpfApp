This is part of a Microservices Restaurant Ordering application that began as a hobby project
so far devided in:

1) OrdersGetAndPost - Azure Functions API 
For managing order updates through Table Queue- orders info flagged as ready 
true / false
(Queuing is currently run locally with azurite emulator during development)

Orders can here be requested with Get (for example with htmltemplates index 
which is as a customer display of the orders in a restaurant) 
or with Post you add a new one, for example 
through the KioskWpfApp.

2) KioskWPfApp - WPF

It is simulating a customer Kiosk experience - communication with the 1) 
Api Post for adding new orders functionality. Customers will receive an
order number.

3) Customer Order Display (htmltemplates - index)

It's purpose is to notify customers of the state of their orders
by tracking it by order number, and the order can be either prepared 
or ready.

future improvements

Using fetch for customer display can be efficient when communicating 
with the API and fast 
enough for the purpose of just displaying orders. It will not have an effect
on the restaurants profit, since it is just for display purpose. However for customer
ordering a faster solution could be improve efficiency of the restaurant in
long term, fetch could lead to a delay since placed orders will not be sent
immediately. so an improvement in the future could be using SignalR or Socket
direclty.       

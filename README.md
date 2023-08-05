# Async__Inn

![](/assets/async-inn-erd.png)


For the **Hotel** entity, the table adds three hotels with their respective names, street addresses, cities, countries, phone numbers, and states.

For the **Room** entity, the table adds three rooms with their names and layout sizes.

For the **Amenity** entity, the table adds three amenities with their names.

## architecture
3 esstinal models (Hotels, Rooms, and Amenities).

3 Interfaces for every model.

service for each of the controllers that implement the appropriate interface.

CRUD operations for evry class.

I Update the Controller to use the appropriate method from the interface rather than the DBContext directly.

## API Requests (CRUD)
![](./assets/GetAll.png)
![](./assets/Get1.png)
![](./assets/post.png)
![](./assets/put.png)
![](./assets/Delete.png)

## Navigation properties
we add a NP to the previos entity so it has an access to each other values.

And here is the CRUD operations after adding NP:

Get:
![](./assets/GetRoom.png)

Post:
![](./assets/PostRoom.png)

Put:
![](./assets/PutRoom.png)

Delete:
![](./assets/DeleteRoom.png)

## DTOs
We add DTO classes and apply the changes to match required DTO features.

Here are some requests after applying DTOs:

![](./assets/1.png)

![](./assets/2.png)

![](./assets/3.png)

![](./assets/4.png)

## Summary comments
I added sommary comments to the service classes, which describe each method task.

## Swagger 
I added swagger to the application, so that the user can easily reach API routes and try using them in an friendly interface. 

Here is how it looks like:
![](./assets/swagger%20general.png)

We can try executing one of them:
![](./assets/HotelPutExecute.png)
And here is the response:
![](./assets/HotelPutResponse.png)

## Unit Test
First, I build Mock DB to deal with the test, then I build some tests to verify the app and its different tasks.

Here is the Test result:
![](./assets/test.png)
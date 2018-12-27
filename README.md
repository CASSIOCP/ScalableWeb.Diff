# ScalableWeb.Diff

A scalable .NET Core web Api to check diff content.

## Getting Started
Runs the **ScalableWeb.Diff.Api** project on your local machine.
You can use either the propted web browser or a api test environment like [Postman](https://www.getpostman.com/) to test the request.

## Usage

If you are running on your browser, a [Swagger](https://swagger.io/) interface will be displayed with the following methods.

<div align="center">
  <img src="https://github.com/CASSIOCP/ScalableWeb.Diff/blob/master/api.png"><br>
</div>

First, use the **Left** and **Right** requests to set up the contents of the comparer.

<div align="center">
  <img src="https://github.com/CASSIOCP/ScalableWeb.Diff/blob/master/left input.png"><br>
</div>

A **int type Id** is required. If the id does not exist, a new one will be created.

A **base64 encoded** string content must be set in the **body** of the request.

If all goes well, you will receive a confirmation message, otherwise your content should probably be in an invalid type.

After that, you can now use the **Get** request to compare the contents of both **Left** and **Right**.

<div align="center">
  <img src="https://github.com/CASSIOCP/ScalableWeb.Diff/blob/master/get.png"><br>
</div>

A **int type Id** is required.

This request will return one of the following results:

```sh
* Contents are the same.
* Contents are not of the same size.
* A Json collection with the diffs (offset and length).
```

## Built With

* [.Net Core](https://dotnet.microsoft.com/download) - The web framework used
* [Swagger](https://swagger.io/) - Api interface
* [LiteDB](http://www.litedb.org/) - Embeded NoSQL databse for .NET
* [Newtonsoft](https://www.newtonsoft.com/json) - Json Manipulator


## Author

* **Cássio C. Perin**

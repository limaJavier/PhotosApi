# Store Photo

## Store Photo Request

**Endpoint**

`POST /photos`

**Body**

```json
{
	"name": "pic_name.png",
	"description": "This is an example picture",
	"formFile": "string of bytes representing the image"
}
```

## Store Photo Response

**Status Code**

`201 Created`

**Location**

`{{host}}/photos/{{id}}`

**Body**

```json
{
	"id": "2d767b07-fb0d-4618-8232-14b82e8f4e92",
	"name": "pic_name.png",
	"description": "This is an example picture",
	"lastModifiedDateTime": "2023-04-08T08:00:00",
	"url": "https:/{{host}}/photos/Storage/id_name.png"
}
```

# Get Photo

## Get Photo Request

**Endpoint**

`GET /photos/{{id}}`

## Get Photo Response

**Status Code**

`200 Ok`

**Body**

```json
{
	"id": "2d767b07-fb0d-4618-8232-14b82e8f4e92",
	"name": "pic_name.png",
	"description": "This is an example picture",
	"url": "https:/{{host}}/photos/Storage/id_name.png"
}
```

# Upsert Photo

## Upsert Photo Request

**Endpoint**

`PUT /photos/{{id}}`

**Body**

```json
{
	"name": "pic_renamed.png",
	"description": "This picture has been modified"
}
```

## Upsert Photo Response

**Status Code**

`204 No Content`

**Body**

```json
{
	"id": "2d767b07-fb0d-4618-8232-14b82e8f4e92",
	"name": "pic_renamed.png",
	"description": "This picture has been modified",
	"lastModifiedDateTime": "2023-04-08T08:00:00",
	"url": "https:/{{host}}/photos/Storage/id_renamed.png"
}
```

# Delete Photo

## Delete Photo Request

**Endpoint**

`DELETE /photos/{{id}}`

## Delete Photo Response

**Status Code**

`204 No Content`

# Download Photo

## Download Photo Request

**Endpoint**

`GET /photos/Download/{{id}}`

## Download Photo Response

**Status Code**

`200 Ok`

**Body**

```json
{
	fileContentResult: "string of bytes representing the image"
}
```

# Get Photos

## Get Photos Request

**Endpoint**

`GET /photos/`

## Get Photos Response

**Status Code**

`200 Ok`

**Body**

An array of this type of json

```json
{
	"id": "2d767b07-fb0d-4618-8232-14b82e8f4e92",
	"name": "pic_name.png",
	"description": "This is an example picture",
	"lastModifiedDateTime": "2023-04-08T08:00:00",
	"url": "https:/{{host}}/photos/Storage/id_name.png"
}
```
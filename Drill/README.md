Drill
=====
A dotnet core application for scraping oil price data from the Web and
placing it into a database.


Requirements
------------
* Ubuntu 16.04
* A Postgres database with a schema named `price_data` and a table named `oil`
with the following definition:

```sql
CREATE TABLE price_data.oil (
   id serial primary key,
   price numeric(7,2),
   sample_date date,
   sample_time time,
   source varchar(255),
   uom varchar(64)
);
```


Installation
------------
The Debian package is found in the `dist` directory.

```
> sudo dpkg -i oildrill-1.0.0.deb
```

To configure file is found at `/etc/oildrill`

Use
---
To fetch the oil price from the Web, simply execute the program.

```
> oildrill
Going to the Web for the oil price...
done
```


Components
----------
### Drill
The *Drill* application class uses multiple modules for extracting price data
and inputting it into a database.

### econContext
The *econContext* class provides access to the data layer (using Entity
Framework for dotnet core).

### Rig
The *Rig* class is what does the Web scraping. To change the source where
the oil price data comes from, either edit this class to parse a different
website or swap it out with another "rig".


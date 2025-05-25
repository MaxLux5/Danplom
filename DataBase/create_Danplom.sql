create database Danplom;
use Danplom;

create table Requests
(
  id int AUTO_INCREMENT primary KEY,
  required_quantity int,
  time_to_complete int,
  request_status int DEFAULT 0,
  detail_id varchar(5) references Details(id),
  user_id int references Users(id)
);

create table Details
(
  id varchar(5) primary KEY,
  detail_name varchar(20)
);

create table Users
(
  id int AUTO_INCREMENT primary KEY,
  user_name varchar(25),
  user_role int,
  login varchar(15) not null,
  password varchar(15) not null
);
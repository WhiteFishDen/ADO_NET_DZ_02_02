create table type_of_stationery 
(
	id int generated always as identity primary key,
	name varchar (20) not null
);

create table stationery
(
	id int generated always as identity primary key,
	name varchar (20) not null,
	price numeric not null,
	id_type int references type_of_stationery(id) on delete cascade not null
);

create table sales_manager
(
	id int generated always as identity primary key,
	firstname varchar(20) not null,
	lastname varchar(20) not null,
	profit numeric default '0'
);



create table buyer_company
(
	id int generated always as identity primary key,
	name varchar(40) not null,
	adress varchar
);

create table sales
(
	id int generated always as identity primary key,
	date_of_sale date,
	id_stationery int references stationery(id) on delete cascade not null,
	id_sales_manager int references sales_manager(id) on delete cascade not null,
	id_buyer_company int references buyer_company(id) on delete cascade not null,
	quantity_products int default '0' not null
);



insert into type_of_stationery (name)
values('Paper products'), ('Office tools'), ('Writing instruments'), ('Art supplies');

insert into stationery (name, price, id_type)
values ('pen', 12, 3),
		('pencil', 15.50, 3),
		('office paper', 530.99, 1),
		('art brush', 34.85, 4),
		('text highlighter', 170.40, 3),
		('stapler', 390.36, 2),
		('notebook', 93.29, 1),
		('clip', 75.90, 2);
	
insert into sales_manager (firstname, lastname)
values ('Mark', 'Hemil'), ('Lisa', 'Simpson'), ('Ned', 'Stark');

insert into buyer_company (name, adress)
values ('Academy Step', 'NY/Green avenu 12'),
		('BigDealCompany', '1337 Nice Avenue 13B Los Angeles CA 1485-2484'),
		('RolfState', '17 Broke Avenue 65B New Gampshir, A84'),
		('DarkProjectLTD', '7 One Avenue 32C Pasadena, CA3284-1237');
	delete from sales;
insert into sales (date_of_sale, id_stationery, id_sales_manager, id_buyer_company, quantity_products)
values('2024-01-29', 2, 2, 2, 7);

insert into sales (date_of_sale, id_stationery, id_sales_manager, id_buyer_company, quantity_products)
values('2024-01-23', 4, 3, 3, 5);

insert into sales (date_of_sale, id_stationery, id_sales_manager, id_buyer_company, quantity_products)
values('2024-01-25', 5, 1, 1, 10);

insert into sales (date_of_sale, id_stationery, id_sales_manager, id_buyer_company, quantity_products)
values('2024-01-30', 3, 1, 1, 9);



insert into sales (date_of_sale, id_stationery, id_sales_manager, id_buyer_company, quantity_products)
values('2024-02-01', 3, 2, 3, 4);




insert into sales (date_of_sale, id_stationery, id_sales_manager, id_buyer_company, quantity_products)
values('2024-02-03', 1, 2, 2, 5);

delete from sales;

alter table sales 
add column profit_product numeric;

create or replace function update_sum_profit_product() returns trigger as 
$$
begin 
	update sales set profit_product = (select st.price from stationery st join sales s on st.id=s.id_stationery order by s.id desc limit 1) * quantity_products
	where sales.id = (select sales.id from sales order by sales.id desc limit 1);
return new;
end;
$$language plpgsql;

create or replace trigger update_sales 
after insert on sales
for each row 
execute procedure update_sum_profit_product();




create or replace function update_profit_manager() returns trigger as 
$$
begin
	update sales_manager set profit = profit + (select price from stationery s join sales on s.id = sales.id_stationery
												order by sales.id desc 
												limit 1) * (select s.quantity_products  from sales s order by s.id desc limit 1) 
	where sales_manager.id = (select id_sales_manager from sales s 
                              order by s.id desc
                              limit 1);
return new;
end;
$$ language plpgsql;

create or replace trigger insert_sales
after insert on sales
for each row 
execute procedure update_profit_manager();

--------------------------АРХИВНЫЕ ТАБЛИЦЫ-----------------------------------

create table arch_type_of_stationery 
(
	id int generated always as identity primary key,
	name varchar (20) not null
);

create table arch_stationery
(
	id int generated always as identity primary key,
	name varchar (20) not null,
	price numeric not null
);

create table arch_sales_manager
(
	id int generated always as identity primary key,
	firstname varchar(20) not null,
	lastname varchar(20) not null,
	profit numeric default '0'
);

create table arch_buyer_company
(
	id int generated always as identity primary key,
	name varchar(40) not null,
	adress varchar
);
 ---------------------------ТРИГГЕРЫ для переноса в архивные таблицы----------------------------

create or replace function into_arch_type_of_stationery() returns trigger as 
$$
begin 
	insert into arch_type_of_stationery(id ,name) OVERRIDING SYSTEM VALUE
	values (old.id,old.name);
return old;
end;
$$language plpgsql;


create or replace trigger delete_type_of_stationery
after delete on type_of_stationery
for each row
execute procedure into_arch_type_of_stationery();

create or replace function into_arch_stationery() returns trigger as 
$$
begin
	
	insert into arch_stationery(id, name, price) OVERRIDING SYSTEM VALUE
	values (old.id, old.name, old.price);
return old;
end;
$$language plpgsql;



create or replace trigger delete_stationery
after delete on stationery
for each row
execute procedure into_arch_stationery();


create or replace function into_arch_sales_manager() returns trigger as 
$$
begin 
	insert into arch_sales_manager(id, firstname, lastname, profit) OVERRIDING SYSTEM VALUE
	values (old.id, old.firstname, old.lastname, old.profit);
return old;
end;
$$language plpgsql;


create or replace trigger delete_sales_manager
after delete on sales_manager
for each row
execute procedure into_arch_sales_manager();

create or replace function into_arch_buyer_company() returns trigger as 
$$
begin 
	insert into arch_buyer_company(id, name, adress) OVERRIDING SYSTEM VALUE
	values (old.id, old.name, old.adress);
return old;
end;
$$language plpgsql;


create or replace trigger delete_buyer_company
after delete on  buyer_company
for each row
execute procedure into_arch_buyer_company();




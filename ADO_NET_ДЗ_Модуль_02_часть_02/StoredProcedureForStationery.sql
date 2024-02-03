
--процедуры для вставки данных:

create or replace procedure insert_type_of_stationery(p_name varchar)
as
$$
begin 
	insert into type_of_stationery (name) values (p_name);
end;
$$ language plpgsql;

create or replace procedure insert_stationery(p_name varchar, p_price numeric, p_id_type int)
as
$$
begin 
	insert into stationery (name, price, id_type) values (p_name, p_price, p_id_type);
end;
$$ language plpgsql;

create or replace procedure insert_buyer_company(p_name varchar, p_adress varchar) as 
$$
begin 
	insert into buyer_company (name, adress) values (p_name, p_adress);
end;
$$
language plpgsql;

create or replace procedure insert_sales_manager (p_firstname varchar, p_lastname varchar, p_profit numeric) as 
$$
begin 
	insert into  sales_manager (firstname, lastname, profit) values (p_firstname, p_lastname, p_profit);
end;
$$
language plpgsql;

create or replace procedure insert_sales(p_date_of_sale date,p_id_stationery int, p_id_sales_manager int, p_id_buyer_company int, p_quantity_products int) as 
$$
begin 
	insert into sales (date_of_sale,  id_stationery, id_sales_manager, id_buyer_company, quantity_products)
	values (p_date_of_sale, p_id_stationery, p_id_sales_manager, p_id_buyer_company, p_quantity_products);
end;
$$
language plpgsql;

--процедуры для обновления данных:

create or replace procedure update_type_of_stationery(p_name varchar, p_id int)
as 
$$
begin 
	update type_of_stationery set name = p_name where id=p_id;
end;
$$ language plpgsql;


create or replace procedure update_stationery(p_name varchar, p_price numeric, p_id_type int, p_id int)
as 
$$
begin 
	update stationery set name = p_name, price = p_price, id_type = p_id_type  where id=p_id;
end;
$$ language plpgsql;

create or replace procedure update_buyer_company(p_name varchar, p_adress varchar, p_id int)
as 
$$
begin 
	update buyer_company set name = p_name, adress = p_adress where id=p_id;
end;
$$ language plpgsql;

create or replace procedure update_sales_manager(p_firstname varchar,p_lastname varchar, p_profit numeric, p_id int)
as 
$$
begin 
	update sales_manager set firstname=p_firstname, lastname=p_lastname, profit=p_profit where id=p_id;
end;
$$ language plpgsql;


create or replace procedure update_sales(p_date_of_sale date, p_id_stationery int, p_id_sales_manager int, p_id_buyer_company int, p_id int)
as 
$$
begin 
	update sales set date_of_sale = p_date_of_sale, id_stationery = p_id_stationery, id_sales_manager=p_id_sales_manager, id_buyer_company=p_id_buyer_company where id=p_id;
end;
$$ language plpgsql;

--процедуры для удаления данных:

create or replace procedure delete_type_of_stationery(p_id int)
as 
$$
begin 
	delete from type_of_stationery where id = p_id;
end;
$$language plpgsql;

create or replace procedure delete_stationery(p_id int)
as 
$$
begin 
	delete from stationery where id = p_id;
end;
$$language plpgsql;

create or replace procedure delete_buyer_company(p_id int)
as 
$$
begin 
	delete from buyer_company where id = p_id;
end;
$$language plpgsql;

create or replace procedure delete_sales_manager(p_id int)
as 
$$
begin 
	delete from sales_manager where id = p_id;
end;
$$language plpgsql;

create or replace procedure delete_sales(p_id int)
as 
$$
begin 
	delete from sales where id = p_id;
end;
$$language plpgsql;
















create view Quantity_max as 
select concat(sm.firstname, ' ', sm.lastname) as "Fullname", sm.profit as "Profit", sum(s.quantity_products) as "Quantity units"
from sales s join sales_manager sm on s.id_sales_manager = sm.id 
group by  concat(sm.firstname, ' ', sm.lastname), sm.profit
order by sum(s.quantity_products) desc
limit 1;

create view Profit_max as
select concat(sm.firstname, ' ', sm.lastname) as "Fullname", sm.profit as "Profit"
from sales_manager sm 
group by  concat(sm.firstname, ' ', sm.lastname), sm.profit
order by sm.profit desc
limit 1;


create view Best_buyer_company as
select bc.name, bc.adress, sm.profit from buyer_company bc
join sales s on bc.id = s.id_buyer_company
join sales_manager sm on sm.id = s.id_sales_manager
order by profit desc limit 1;


create view max_number_type_of_product_sales as
select tos.name, sum(s.quantity_products) as "num_units_sold" from sales s
join stationery st on s.id_stationery = st.id
join type_of_stationery tos 
on tos.id = st.id_type
group by tos.name, st.name, st.price 
order by sum(s.quantity_products) desc limit 1;


create view most_profitable_type_of_product as
select tos.name as "Name type", sum(s.profit_product) as "Profit" from sales s
join stationery st on s.id_stationery = st.id
join type_of_stationery tos on st.id_type = tos.id
group by tos.name order by sum(s.profit_product) desc limit 1;

create view most_popular_products as
select sum(s.quantity_products) as "Quantity", st."name" , st.price  from sales s
join stationery st on s.id_stationery  = st.id 
group by st."name", st.price 
order by sum(s.quantity_products) desc limit 3;

select distinct st."name" as "Stationery name"  from stationery st
join sales s on st.id = s.id_stationery 
where now() >= s.date_of_sale + 5;






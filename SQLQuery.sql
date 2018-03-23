
if exists (select [name] from sys.tables where [name] = 'Member')
drop table Member
go

/*กำหนดตาราง*/
create table Member
(
	id			int identity(1,1) primary key,
	code		varchar(50),
	name		varchar(50),
	position	varchar(50),
	department	varchar(50),
	province	varchar(50),
	birth		datetime
)

/*กำหนด procedure insert ข้อมูล*/

if exists (select*from sys.procedures where [name] = 'sp_memberIn')
drop procedure sp_memberIn
go

create procedure sp_memberIn
(
	@code		varchar(50),
	@name		varchar(50),
	@position	varchar(50),
	@department	varchar(50),
	@province	varchar(50),
	@birth		datetime
)
as
begin
		insert into Member(
					code,
					name,
					position,
					department,
					province,
					birth)
	values(
			@code,
			@name,
			@position,
			@department,
			@province,
			@birth)
end

/*กำหนด select ข้อมูล*/
if exists (select*from sys.procedures where [name] = 'sp_member_Select')
drop procedure sp_member_Select
go

create procedure sp_member_Select
(
	@name		varchar(50) = ''
)
as
begin
		select  id, code, name, position, 
				department, province, birth, 
				DATEDIFF(year, birth, GETDATE()) AS age
		from Member
		where name like @name+'%'
end

/*กำหนด update ข้อมูล*/
if exists (select*from sys.procedures where [name] = 'sp_memberUp')
drop procedure sp_memberUp
go

create procedure sp_memberUp
(
	@id			int,
	@name		varchar(50),
	@position	varchar(50),
	@department varchar(50),
	@province	varchar(50),
	@birth		datetime
)
as
begin
		update Member
		set name		= @name,
			position	= @position,
			department	= @department,
			province	= @province,
			birth		= @birth
		where id = @id
end

/*ลบ ข้อมูล*/
if exists (select*from sys.procedures where [name] = 'sp_memberDel')
drop procedure sp_memberDel
go

create procedure sp_memberDel
(
	@id		int
)
as
begin
		delete from Member
		where id = @id
end


import { DeleteOutlined, EditOutlined } from "@ant-design/icons";
import { Popconfirm, Space, Table, message } from "antd";
import { request, history, useModel } from "@umijs/max";
import dayjs from 'dayjs';

export default function() {
  const {data, loading, fillData} = useModel("useStudentModel")
    const date_format = (value: string | null) => {
        if(!value) return "";
        const date = dayjs(value);

        if(date.format('YYYY') == '1901' && date.format('DD') == '01' && date.format('MM') == '01') return "";

        return date.format('DD.MM.YYYY');
    }
    
    const deleteHandler = (id: number) => {
        request("api/Student", { method: "DELETE", params: { id } }).then(() => {
        message.success("Студент удален")
        const newStudents = data?.students.filter((x : any) => x.id != id)
        const newData = {...data, students: newStudents}
        fillData(newData)
        }).catch(error => {
        message.error("Ошибка при удалении студента");
        console.log(error);
        });
    }

    const columns = [{
        title: "",
        dataIndex: "id"
      },
      {
        title: "Группа",
        dataIndex: "groupId",
        render: (value: number) => data.groups.find((x: any) => x.id == value)?.name
      },
      {
        title: "Имя",
        dataIndex: "firstName"
      },
      {
        title: "Фамилия",
        dataIndex: "lastName"
      },
      {
        title: "Email",
        dataIndex: "email"
      },
      {
        title: "Дата создания",
        dataIndex: "createdAt",
        render: (value: any) => date_format(value)
      },
      {
        title: "Дата обновления",
        dataIndex: "updatedAt",
        render: (value: any) => date_format(value)
      }, 
      {
        title: "Действие",
        key: 'actions',
        render: (row : any) =>  {
        return <Space>
          <a onClick={ () => history.push('/student/edit/' + row.id) }>
            <EditOutlined />
          </a>
          <a>
            <Popconfirm
              title="Вы уверены?"
              description="Удалить студента"
              onConfirm={() => deleteHandler(row.id)}
              onCancel={() => {}}
              okText="Yes"
              cancelText="No"
            >
              <DeleteOutlined />
            </Popconfirm>
          </a>
        </Space>
        }
      }]

    return(
      <>
        {<Table 
          rowKey={"id"}
          loading = {loading}
          columns={ columns } 
          dataSource={ loading ? [] : data?.students } />}
      </>
    );
  }
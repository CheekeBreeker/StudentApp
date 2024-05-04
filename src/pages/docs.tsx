import { DeleteOutlined, EditOutlined } from "@ant-design/icons";
import { request, history } from "@umijs/max";
import { Button, Form, Input, Modal, Popconfirm, Select, Space, Table, message } from "antd";
import React, { useState } from "react";

const DocsPage = () => {

  const [value, setValue] = React.useState<string>("");
  const {data,setData} = useModel("useStudentModel")
  const [groups, setGroups] = React.useState<any>("");
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [form] = Form.useForm();
  
  const updateStudents = () => {
    request('/api/Student/GetAll', {method: 'POST', data: { }}).then(data => {
      setData(data)
    })
  }

  const showModal = () => {
    setIsModalOpen(true);
    form.resetFields();
  };

  React.useEffect(() => {
    
    updateStudents()

    request('/api/Group/GetAll', {method: 'POST', data: { }}).then(data => {
      const groups = data.map((x : any) => ({ value: x.id, label: x.name }))
      setGroups(groups)
    })
  }, []);

  let a = "1";

  const inputOnChange = (event : any) => {
    setValue(event.target.value);
  }

  const searchHandler = (data : any) => {
    request('/api/Student/GetAll', {method: 'POST', data}).then(data => {
      setData(data)
      console.log(data)
    }).catch(error => {
      message.error("Ошибка при фильтрации студентов");
      console.log(error);
      setIsModalOpen(false)
    });
  }

  const addHandler = (data: any) => {
    console.log(JSON.stringify(data));
    request('/api/Student', {method: 'PUT', data}).then(data => {
      setIsModalOpen(false)
      updateStudents()
    }).catch(error => {
      message.error("Ошибка при добавлении студента");
      console.log(error);
      setIsModalOpen(false)
    });
  }
  
  return (
    <div>
      <Button type="primary" onClick={showModal} style={ {marginBottom: "12px"}}>
        Новый студент
      </Button>
    
      <Form layout="inline" onFinish={searchHandler} >
        <Form.Item 
          name="groupId" 
          label="Группа" 
          style={ {marginBottom: "12px"}}>
            <Select 
              allowClear
              options={groups} 
              style={{width: "150px"}} 
            />
        </Form.Item>

        <Form.Item name="firstName" 
          label="Имя" 
          style={ {marginBottom: "12px"}}>
            <Input style={{width: "150px"}}/>
        </Form.Item>
        <Form.Item name="lastName"  
          label="Фамилия" 
          style={ {marginBottom: "12px"}}>
            <Input style={{width: "150px"}}/>
        </Form.Item>
        <Form.Item name="email" 
          label="Email" 
          style={ {marginBottom: "12px"}}>
            <Input style={{width: "150px"}}/>
        </Form.Item>
        <Button type="primary" 
          htmlType="submit" 
          style={ {marginBottom: "12px"}}>
            Найти
        </Button>
      </Form>

      
      
      {/* Модалки */}
      <Modal title="Новый студент" 
        open={isModalOpen} footer={null} 
        onOk={() => setIsModalOpen(false)}
        onCancel={() => setIsModalOpen(false)}>
          <Form layout="horizontal" onFinish={addHandler} form={form} >
            <Form.Item 
              name="groupId" 
              label="Группа" 
              style={ {marginBottom: "12px"}}>
                <Select 
                  id="groupIdModal"
                  allowClear
                  options={groups} 
                />
            </Form.Item>

            <Form.Item name="firstName" 
              label="Имя" 
              style={ {marginBottom: "12px"}}>
                <Input id="firstNameModal" />
            </Form.Item>
            <Form.Item name="lastName"  
              label="Фамилия" 
              style={ {marginBottom: "12px"}}>
                <Input id="lastNameModal" />
            </Form.Item>
            <Form.Item name="email" 
              label="Email" 
              style={ {marginBottom: "12px"}}>
                <Input id="emailModal" />
            </Form.Item>
            <Form.Item name="password" 
              label="Пароль" 
              style={ {marginBottom: "12px"}}>
                <Input.Password id="passwordModal" />
            </Form.Item>
            <Button type="primary" 
              htmlType="submit" 
              style={ {marginBottom: "12px"}}>
                Добавить
            </Button>
          </Form>
      </Modal>
    </div>
  );
};

export default DocsPage;

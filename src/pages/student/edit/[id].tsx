import { request, useParams, history } from "@umijs/max";
import { Button, Form, Input, Select, message } from "antd";
import React from "react";

const DocsPage = (props : any) => {

  const [data, setData] = React.useState<any>("");
  const [form] = Form.useForm();
  const [groups, setGroups] = React.useState<any>("");
  
  const params = useParams()
  
  React.useEffect(() => {
    request('/api/Student/GetOne', {method: 'GET', params}).then(data => {
      form.setFieldsValue(data)
    })

    request('/api/Group/GetAll', {method: 'POST', data: { }}).then(data => {
      const groups = data.map((x : any) => ({ value: x.id, label: x.name }))
      setGroups(groups)
    })
  }, []);

  const editHandler = (data: any) => {
    console.log(JSON.stringify(data));
    request('/api/Student', {method: 'POST', data}).then(data => {
      history.push('/docs')
    }).catch(error => {
      message.error("Ошибка при добавлении студента");
      console.log(error);
    });
  }

  return (
    <div>
      <Form layout="horizontal" onFinish={editHandler} form={form} >

        <Form.Item name="id" hidden noStyle />

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
            Сохранить
        </Button>
      </Form>
    </div>
  );
};

export default DocsPage;

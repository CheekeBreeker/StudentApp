import TableStudents from "@/components/tables/TableStudents";
import ModalStudentAdd from "@/components/tables/modals/ModalStudentAdd";
import { DeleteOutlined, EditOutlined } from "@ant-design/icons";
import { request, history, useModel } from "@umijs/max";
import { Button, Form, Input, Modal, Popconfirm, Select, Space, Table, message } from "antd";
import React, { useState } from "react";

const DocsPage = () => {

  const { initialState, setInitialState, refresh } = useModel("@@initialState")
  const { showModal, updateStudents, searchHandler} = useModel("useStudentModel")
  const { GetGroups, groups} = useModel("useGroupsModel")
  
  React.useEffect(() => {
    
    updateStudents()
    GetGroups()

  }, []);
  
  return (
    <div>
      Здравствуйте {initialState?.username}
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

      <TableStudents />
      <ModalStudentAdd />
    </div>
  );
};

export default DocsPage;

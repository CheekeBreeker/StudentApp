import { useModel } from "@umijs/max";
import { Button, Form, Input, Modal, Select } from "antd";
import { useState } from "react";

export default function HomePage() {
    const { isModalOpen, form, setModalState, addHandler } = useModel("useStudentModel")
    const { groups } = useModel("useGroupsModel")
    
    return(
        <Modal title="Новый студент" 
        open={isModalOpen} footer={null} 
        onOk={() => setModalState(false)}
        onCancel={() => setModalState(false)}>
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
    );
  }
  
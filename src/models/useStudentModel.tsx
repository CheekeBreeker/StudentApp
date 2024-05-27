import { request } from "@umijs/max";
import { Form, message } from "antd";
import React, { useState } from "react";

export default function UseStudentModel() {
    const [data, setData] = React.useState<any>();
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [loading, setLoading] = React.useState<boolean>(true);
    const [form] = Form.useForm();

    const fillData = (data: any) => setData(data)
    const setModalState = (state: boolean) => setIsModalOpen(state)

    const updateStudents = () => {
        setLoading(true)
        request('/api/Student/GetAll', {method: 'POST', data: { }}).then(data => {
            fillData(data)
            console.log(data)
            setLoading(false)
        }).finally(() => {
          setLoading(false)
        })
    }
    
    const searchHandler = (data : any) => {
        setLoading(true)
        request('/api/Student/GetAll', {method: 'POST', data}).then(data => {
            fillData(data)
            console.log(data)
            setLoading(false)
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

    const showModal = () => {
        setModalState(true);
        form.resetFields();
      };

    return {
        data, 
        loading,
        isModalOpen,
        form,
        fillData,
        setModalState,
        updateStudents,
        searchHandler,
        addHandler,
        showModal
    }
  }
  
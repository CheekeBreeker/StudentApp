import { request, useModel } from "@umijs/max";
import { Button, Form, Input } from "antd";

export default function HomePage() {
  const { setInitialState } = useModel("@@initialState")
  
  const loginHandler = (data : any) => {
    console.log(data)

    request('/api/auth', { method: "POST", data }).then((result : any) => {
      localStorage.setItem('username', result?.email);
      localStorage.setItem('password', result?.password);
    })
  }

  return(
    <Form layout="vertical" onFinish={loginHandler}>
      <Form.Item name="login" label="Имя пользователя" rules={[
        { required: true, message: "Введите ваше имя" }
      ]} >
        <Input />
      </Form.Item>
      <Form.Item name="password" label="Пароль" rules={[
        { required: true, message: "Введите ваш пароль" }
      ]} >
        <Input.Password />
      </Form.Item>
      <Button htmlType="submit" type="primary">Войти</Button>
    </Form>
  );
}

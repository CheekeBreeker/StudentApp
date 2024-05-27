import { request } from "@umijs/max";
import { message } from "antd";
import React, { useState } from "react";

export default function UseStudentModel() {
    const [groups, setGroups] = React.useState<any>();

    const GetGroups = () => {
        request('/api/Group/GetAll', { method: 'POST', data: { } }).then(data => {
            const groups = data.map((x : any) => ({ value: x.id, label: x.name }))
            setGroups(groups)
        })
    }

    return {
        groups,
        GetGroups
    }
  }
  
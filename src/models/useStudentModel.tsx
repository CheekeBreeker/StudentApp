import React from "react";

export default function UseStudentModel() {
    const [data, setData] = React.useState<any>();

    return {
        data, 
        setData
    }
  }
  
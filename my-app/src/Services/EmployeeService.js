import http from "../http";  
class EmployeeService {  
    getAllPositions() {  
        return http.get("/getWorkPositions");  
      }  
      getAllEmployees() {  
        return http.get("/getActiveEmployees");  
      }
      createNewEmployee(employeeModel) {
          return http.post("/createEmployee", JSON.stringify(employeeModel));
      }
        
}  
export default new EmployeeService();  
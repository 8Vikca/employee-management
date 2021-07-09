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
      createNewWorkPosition(workPositionModel) {
        console.log(workPositionModel);
        return http.post("/createWorkPosition", JSON.stringify(workPositionModel));
    }
      sendEditedEmployee(employeeModel){
        console.log(employeeModel);
        return http.post("/editEmployee", JSON.stringify(employeeModel));
    }
        
}  
export default new EmployeeService();  
import http from "../http";
class EmployeeService {
  getAllEmployees() {
    return http.get("/getActiveEmployees");
  }
  createNewEmployee(employeeModel) {
    return http.post("/createEmployee", JSON.stringify(employeeModel));
  }
  editEmployee(employeeModel) {
    return http.post("/editEmployee", JSON.stringify(employeeModel));
  }
  deleteEmployee(employeeModel) {
    return http.post("/deleteEmployee", JSON.stringify(employeeModel));
  }
  archiveEmployee(employeeModel) {
    return http.post("/archiveEmployee", JSON.stringify(employeeModel));
  }

}
export default new EmployeeService();
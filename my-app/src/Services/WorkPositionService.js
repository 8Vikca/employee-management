import http from "../http";  
class WorkPositionService {  
    getAllPositions() {  
        return http.get("/getWorkPositions");  
      }  
      createNewWorkPosition(workPositionModel) {
        console.log(workPositionModel);
        return http.post("/createWorkPosition", JSON.stringify(workPositionModel));
    }
        
}  
export default new WorkPositionService();  
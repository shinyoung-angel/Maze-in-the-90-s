mergeInto(LibraryManager.library, {
 
    //�г��� �ߺ��˻�
    CheckNickname: function(name) {
    
    //���� �õ��ϴ� �г���
    var parsedNick = Pointer_stringify(name);
    console.log(parsedNick);
    // ������ ���
    var nameRef = firebase.database().ref('users'); //��ü ����
    // ��� (0 -> �ߺ� ���� ���� �Ұ���, 1 -> �ߺ� ���� ���� ����)
    var result = 1

    firebase.database().ref(nameRef).once('value').then(function(list) {
    //�ϳ��� ����
     list.forEach(function (user) {
        console.log(user.val());
        console.log(user.val().nickname);

        //�ߺ� ����
        if (user.val().nickname == parsedNick){
            console.log('�ߺ�');
            result = 0
        }

        //�ƹ��͵� �Է����� ����
        if (parsedNick == ""){
            console.log('nothing....');
            result = 3
        }
      });
      //�˻� �� ����Ƽ�� ��� ����
      console.log(result);
      console.log(typeof result);
      window.unityInstance.SendMessage('SignUpHandler', 'CheckedName', result);
     });

   },

   //�г��� �ߺ��˻�(�Ҽ�)
    CheckNicknameForSocial: function(name) {
    
    //���� �õ��ϴ� �г���
    var parsedNick = Pointer_stringify(name);
    console.log(parsedNick);
    // ������ ���
    var nameRef = firebase.database().ref('users'); //��ü ����
    // ��� (0 -> �ߺ� ���� ���� �Ұ���, 1 -> �ߺ� ���� ���� ����)
    var result = 1

    firebase.database().ref(nameRef).once('value').then(function(list) {
    //�ϳ��� ����
     list.forEach(function (user) {
        console.log(user.val());
        console.log(user.val().nickname);

        //�ߺ� ����
        if (user.val().nickname == parsedNick){
            console.log('�ߺ�');
            result = 0
        }
        //�ƹ��͵� �Է����� ����
        if (parsedNick == ""){
            console.log('nothing....');
            result = 3
        }
      });
      //�˻� �� ����Ƽ�� ��� ����
      console.log(result);
      console.log(typeof result);
      window.unityInstance.SendMessage('LoginHandler', 'CheckedNameForSocial', result);
     });

   },

   //�г��� ���� �ߺ��˻�
   CheckNicknameForChange: function(name) {
    
    //���� �õ��ϴ� �г���
    var parsedNick = Pointer_stringify(name);
    console.log(parsedNick);

    //���� �����г���
    var currNick = firebase.auth().currentUser.displayName;

    // ������ ���
    var nameRef = firebase.database().ref('users'); //��ü ����
    // ��� (0 -> �ߺ� ���� ���� �Ұ���, 1 -> �ߺ� ���� ���� ����, 2 -> ���� ����)
    var result = 1

    firebase.database().ref(nameRef).once('value').then(function(list) {
    //�ϳ��� ����
     list.forEach(function (user) {
        console.log(user.val());
        console.log(user.val().nickname);

        //�ߺ� ����
        if (user.val().nickname == parsedNick){
            console.log('overlap');
            result = 0
        }

        //���� �г��Ӱ� ����
        if (currNick == parsedNick){
            console.log('overlap current nickname');
            result = 2
        }

        //�ƹ��͵� �Է����� ����
        if (parsedNick == ""){
            console.log('nothing....');
            result = 3
        }

      });
      //�˻� �� ����Ƽ�� ��� ����
      console.log(result);
      console.log(typeof result);
      window.unityInstance.SendMessage('LobbyHandler', 'CheckedNameForChange', result);
     });

   },


    //���� ������ �� ����Ƽ���� ���� ������ ���̾�̽��� ������(���常)
    PostGameRecord: function(json) {
    //unity���� string���� ���� json
    var parsedJSON = Pointer_stringify(json);
        
    //string
    console.log(parsedJSON);
    console.log(typeof parsedJSON);

    //string�� json ������Ʈ�� ��ȯ
    var obj = JSON.parse(parsedJSON);
    console.log(obj);
    console.log(typeof obj);
    
    //������Ʈ���� �ʿ��� value ���� ã�Ƽ� ������ ����
    const players = obj.totalPlayers;
    const rank = obj.rank;    
    const name = obj.nickName;
    const time = obj.time;
    const mode = obj.gameMode;
    const map = obj.gameMap;


    //DB ���� ��δ� rank/���(1/2)/��/�г���
    //������ �����ʹ� {time : �ɸ� �ð�}
    var rankRef = firebase.database().ref('rank/' + mode + '/' + map + '/' + name); //��ü ��ŷ
    var recordRef = firebase.database().ref('record/' + name); //���������� ������ ��������

    //��ŷ������ { name: ��¼��, time: 12 }
    var rankData = new Object();
    rankData.name = name;
    rankData.time = time;

    //���������� { mode: ��¼��, map: ��¼��, players: ���, rank: ���, time: �ð� }
    var recordData = new Object();
    recordData.mode = mode;
    recordData.map = map;
    recordData.players = players;
    recordData.rank = rank;
    recordData.time = time;

    //��ü ��ŷ ���̺� ������Ʈ(�ش� ������ ����� �̹� �ִ� ��� �� ª�� ������� ��ü�� ��)
    rankRef.get().then(function(snapshot) {
    //�ش� ��ο� ����� �̹� ����
    if (snapshot.exists()) {
        console.log(snapshot.val());

        //�ð� ��
        //������ �ִ� ����� ���ų� �� ª�ٸ� �������� ����
        if (snapshot.val().time <= time){
            console.log('time not replaced...');
        } 
        //��� ���������� ������ ������ ��ü��
        else{
            firebase.database().ref(rankRef).update(rankData).then(function(unused) {
            console.log('time replaced!');
            });
        }

        //�ش� ��ο� ����� ����(�ش� ���, �ʿ��� ù ������ ���)
    } else {
        firebase.database().ref(rankRef).update(rankData).then(function(unused) {
        console.log('rank post completed!');
        });
        
    
    }
    });

   },

   //�������� �� ���� ������
    PostMyRecord: function(json) {
        //unity���� string���� ���� json
        var parsedJSON = Pointer_stringify(json);
        
        //string�� json ������Ʈ�� ��ȯ
        var obj = JSON.parse(parsedJSON);
    
        //������Ʈ���� �ʿ��� value ���� ã�Ƽ� ������ ����
        const players = obj.totalPlayers;
        const rank = obj.rank;    
        const name = obj.nickName;
        const time = obj.time;
        const mode = obj.gameMode;
        const map = obj.gameMap;


        //DB ���� ��δ� rank/���(1/2)/��/�г���
        //������ �����ʹ� {time : �ɸ� �ð�}
        var recordRef = firebase.database().ref('record/' + name); //���������� ������ ��������

        //���������� { mode: ��¼��, map: ��¼��, players: ���, rank: ���, time: �ð� }
        var recordData = new Object();
        recordData.mode = mode;
        recordData.map = map;
        recordData.players = players;
        recordData.rank = rank;
        recordData.time = time;


        //���� ���� ���̺� ������Ʈ(����� ����� ���� ��� �߰�)
        firebase.database().ref(recordRef).push().set(recordData).then(function(unused) {
        console.log('record post completed!');
        });
   },


   //��ŷ������ �� ����
   SetByInfo: function(mode, map) {

   //���̾�̽��� ������ ���� string���� �Ľ�
   var parsedMode = Pointer_stringify(mode);
   var parsedMap = Pointer_stringify(map);
   console.log(parsedMode);
   console.log(parsedMap);

    //�ش��ϴ� ����� TOP 10 ��ŷ �� �о����
    firebase.database().ref('rank/' + parsedMode + '/' + parsedMap).orderByChild('time').limitToFirst(10).once('value').then(function(list) {
    console.log(list.length);

    //���ĵ� �����͸� �������� ���ؼ� �ϳ��ϳ��� ����
     list.forEach(function (score) {
        console.log(score.val());
        //���ӵ����͸� �ٽ� ����Ƽ�� ����
        window.unityInstance.SendMessage('RankingHandler', 'SetUp', JSON.stringify(score.val()));
        });
     });
   },

   GetRecords: function(username) {
        var parsedUsername = Pointer_stringify(username);

        //�ش��ϴ� ������ ���� �о����
        firebase.database().ref('record/' + parsedUsername).once('value').then(function(list) {
        //���ĵ� �����͸� �������� ���ؼ� �ϳ��ϳ��� ����
        list.forEach(function (record) {
        console.log(record.val());
        //���ӵ����͸� �ٽ� ����Ƽ�� ����
        window.unityInstance.SendMessage('MyPageHandler', 'SetUp', JSON.stringify(record.val()));
        });
     });
   },

   GetRanks: function(username) {
        var parsedUsername = Pointer_stringify(username);

        // ������ ���
        var rankRef = firebase.database().ref('rank'); //��ü ����

        firebase.database().ref(rankRef).once('value').then(function(mode) {
        mode.forEach(function (map) {
        console.log(mode.val());
        console.log(mode.val().name);
        });
      });
   },

   UpdateCharacter: function(charIdx) {

        console.log("updating character");
        console.log(charIdx);
        console.log(typeof charIdx);

        var user = firebase.auth().currentUser;

        var data = { character : charIdx };

        //Realtime Database���� ������Ʈ
        firebase.database().ref('users/' + user.uid).update(data);
        console.log("character update complete!");
   },


});
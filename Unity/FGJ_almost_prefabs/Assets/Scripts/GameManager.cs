using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private int level;
    private int enemyNum;
    private int friendNum;


    public GameObject m_goBackgroundSprite;
    public Component m_comBackgroundSprite;
    //public Sprite m_spBackgroundSprite;

    public List<GameObject> m_goList_MobInst = new List<GameObject>();
    public List<GameObject> m_goList_HandleInst = new List<GameObject>();
    public List<GameObject> m_goList_MapInst = new List<GameObject>();
    public System.Random rnd;

    public void Start()
    {
        level = 1;
        m_goBackgroundSprite = gameObject;
        m_comBackgroundSprite = m_goBackgroundSprite.GetComponent<SpriteRenderer>();
        rnd = new System.Random(System.DateTime.Now.Millisecond);
    }

    private void init()
    {

    }

    private int inner_delay = 0;
    public void Update()
    {
        inner_delay += 1;
        if (inner_delay >= 10)
        {
            inner_delay = 0;
            CreateMapInst(new Vector3(rnd.Next(0, 100), rnd.Next(0, 10)));
        }
    }



    #region _prefab_inst_

    public void CreateMobInst(Vector3 mob_pos)
    {
        GameObject inst = Instantiate(Resources.Load("Prefab/Mobs/Mob", typeof(GameObject))) as GameObject;

        if (inst != null)
        {
            inst.transform.parent = this.transform;
            inst.transform.localPosition = mob_pos;
            m_goList_MobInst.Add(inst);
            Debug.Log("CreateMobInst> Add A New Mob Inst!");
        }
        else
        {
            Debug.LogWarning("CreateMobInst> Cannot Create Mob!");
        }
    }

    public void ClearAllMobInst()
    {
        if (m_goList_MobInst == null)
        {
            return;
        }

        foreach (var item in m_goList_MobInst)
        {
            Destroy(item);
        }
        m_goList_MobInst.Clear();
    }

    public void CreateHandleInst(Vector3 handle_pos)
    {
        GameObject inst = Instantiate(Resources.Load("Prefab/Handle1", typeof(GameObject))) as GameObject;

        if (inst != null)
        {
            inst.transform.parent = this.transform;
            inst.transform.localPosition = handle_pos;
            m_goList_HandleInst.Add(inst);
            Debug.Log("CreateHandleInst> Add A New Handle Inst!");
        }
        else
        {
            Debug.LogWarning("CreateHandleInst> Cannot Create Handle!");
        }
    }

    public void ClearAllHandleInst()
    {
        if (m_goList_HandleInst == null)
        {
            return;
        }

        foreach (var item in m_goList_HandleInst)
        {
            Destroy(item);
        }
        m_goList_HandleInst.Clear();
    }

    public void CreateMapInst(Vector3 map_pos)
    {
        //GameObject inst = Instantiate(Resources.Load("Prefab/Map1", typeof(GameObject))) as GameObject;
        Object res = Resources.Load("Floor2", typeof(GameObject));
        if (res == null)
        {
            Debug.Log("CreateMapInst> Resources Load Failed!");
            return;
        }
        GameObject inst = Instantiate(res) as GameObject;

        if (inst != null)
        {
            inst.transform.parent = this.transform;
            inst.transform.localPosition = map_pos;
            m_goList_MapInst.Add(inst);
            Debug.Log("CreateMapInst> Add A New Map Inst!");
        }
        else
        {
            Debug.LogWarning("CreateMapInst> Cannot Create Map!");
        }
    }

    public void ClearAllMapInst()
    {
        if (m_goList_MapInst == null)
        {
            return;
        }

        foreach (var item in m_goList_MapInst)
        {
            Destroy(item);
        }
        m_goList_MapInst.Clear();
    }

    #endregion
}

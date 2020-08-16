using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool _leftMove = true;
    private float _originX;
    private float _width;

    // Start is called before the first frame update
    void Start()
    {
        _originX = this.gameObject.transform.position.x;
        _width = this.gameObject.GetComponent<RectTransform>().rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vct3 = gameObject.transform.position;
        vct3.x += _leftMove ? -SysDefine.Instance.enemyMoveSpeed * Time.deltaTime : SysDefine.Instance.enemyMoveSpeed * Time.deltaTime;
        gameObject.transform.position = vct3;
        if (vct3.x - _originX > _width || _originX - vct3.x > _width)
            _leftMove = !_leftMove;
    }
}

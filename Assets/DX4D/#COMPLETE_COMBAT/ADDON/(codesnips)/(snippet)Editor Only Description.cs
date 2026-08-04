/* Shows a description in the editor, but does not include it in the build. Perfect for lightweight plugins.

        #region DESCRIPTION
#if UNITY_EDITOR
        [SerializeField][TextArea(1,5)] string componentDescription = 
            ""
            + ""
            + "";
#endif
        #endregion

 */
---
name: Usability Test Plan Template
description: Template para crear usability test plans con scenarios, metrics, scripts
category: ux
version: 1.0.0
author: mjcuadrado-net-sdk
tags: [ux, usability-testing, test-plan, metrics, analysis]
---

# Usability Test Plan: {{feature_name}}

**Feature:** {{feature_name}}
**Test Date:** {{test_date}}
**Test Facilitator:** {{facilitator}}
**Prototype Fidelity:** {{fidelity}} _(Low, Medium, High)_
**Test Method:** {{test_method}} _(Moderated, Unmoderated, Remote, In-person)_

---

## 🎯 Test Objectives

### Primary Objectives
1. **{{objective_1}}**
   - Success Criteria: {{objective_1_criteria}}
   - Priority: {{objective_1_priority}}

2. **{{objective_2}}**
   - Success Criteria: {{objective_2_criteria}}
   - Priority: {{objective_2_priority}}

3. **{{objective_3}}**
   - Success Criteria: {{objective_3_criteria}}
   - Priority: {{objective_3_priority}}

### Research Questions
- {{research_question_1}}
- {{research_question_2}}
- {{research_question_3}}
- {{research_question_4}}

---

## 👥 Participant Criteria

### Target Participants

**Sample Size:** {{sample_size}} participants _(Recommended: 5-8 for qualitative)_

**Demographics:**
- **Age:** {{age_range}}
- **Role:** {{role}}
- **Experience Level:** {{experience_level}} _(Beginner, Intermediate, Advanced)_
- **Tech Savvy:** {{tech_savvy}}
- **Location:** {{location}}

### Inclusion Criteria
- ✅ {{inclusion_1}}
- ✅ {{inclusion_2}}
- ✅ {{inclusion_3}}

### Exclusion Criteria
- ❌ {{exclusion_1}}
- ❌ {{exclusion_2}}
- ❌ {{exclusion_3}}

### Screening Questions

1. **{{screening_q1}}**
   - Expected Answer: {{screening_a1}}

2. **{{screening_q2}}**
   - Expected Answer: {{screening_a2}}

3. **{{screening_q3}}**
   - Expected Answer: {{screening_a3}}

---

## 📋 Test Scenarios & Tasks

### Scenario 1: {{scenario_1_name}}

**Context:** {{scenario_1_context}}

**Task:** {{scenario_1_task}}

**Starting Point:** {{scenario_1_start}}

**Success Criteria:**
- [ ] {{scenario_1_success_1}}
- [ ] {{scenario_1_success_2}}
- [ ] {{scenario_1_success_3}}

**Expected Time:** {{scenario_1_time}}

**Difficulty:** {{scenario_1_difficulty}} _(Easy, Medium, Hard)_

---

### Scenario 2: {{scenario_2_name}}

**Context:** {{scenario_2_context}}

**Task:** {{scenario_2_task}}

**Starting Point:** {{scenario_2_start}}

**Success Criteria:**
- [ ] {{scenario_2_success_1}}
- [ ] {{scenario_2_success_2}}
- [ ] {{scenario_2_success_3}}

**Expected Time:** {{scenario_2_time}}

**Difficulty:** {{scenario_2_difficulty}}

---

### Scenario 3: {{scenario_3_name}}

**Context:** {{scenario_3_context}}

**Task:** {{scenario_3_task}}

**Starting Point:** {{scenario_3_start}}

**Success Criteria:**
- [ ] {{scenario_3_success_1}}
- [ ] {{scenario_3_success_2}}
- [ ] {{scenario_3_success_3}}

**Expected Time:** {{scenario_3_time}}

**Difficulty:** {{scenario_3_difficulty}}

---

### Scenario 4: {{scenario_4_name}}

**Context:** {{scenario_4_context}}

**Task:** {{scenario_4_task}}

**Starting Point:** {{scenario_4_start}}

**Success Criteria:**
- [ ] {{scenario_4_success_1}}
- [ ] {{scenario_4_success_2}}
- [ ] {{scenario_4_success_3}}

**Expected Time:** {{scenario_4_time}}

**Difficulty:** {{scenario_4_difficulty}}

---

### Scenario 5: {{scenario_5_name}}

**Context:** {{scenario_5_context}}

**Task:** {{scenario_5_task}}

**Starting Point:** {{scenario_5_start}}

**Success Criteria:**
- [ ] {{scenario_5_success_1}}
- [ ] {{scenario_5_success_2}}
- [ ] {{scenario_5_success_3}}

**Expected Time:** {{scenario_5_time}}

**Difficulty:** {{scenario_5_difficulty}}

---

## 📊 Success Metrics

### Quantitative Metrics

| Metric | Target | Threshold | Measurement Method |
|--------|--------|-----------|-------------------|
| **Task Completion Rate** | {{completion_target}}% | {{completion_threshold}}% | # completed / # attempted |
| **Time on Task** | {{time_target}} | {{time_threshold}} | Average time per task |
| **Error Rate** | {{error_target}}% | {{error_threshold}}% | # errors / # actions |
| **Clicks to Complete** | {{clicks_target}} | {{clicks_threshold}} | Average clicks per task |
| **SUS Score** | {{sus_target}}/100 | {{sus_threshold}}/100 | System Usability Scale |

### Qualitative Metrics

- **Satisfaction:** Post-task rating (1-5 scale)
- **Perceived Difficulty:** Self-reported (1-5 scale)
- **Confidence:** "How confident are you?" (1-5 scale)
- **Likelihood to Recommend:** NPS score (0-10)

---

## 🎬 Test Script

### Introduction (5 minutes)

```
Hola {{participant_name}}, gracias por participar en esta sesión de testing.

Mi nombre es {{facilitator}} y hoy vamos a probar {{feature_name}}.

Antes de empezar, quiero aclarar algunas cosas:
- Estamos probando el sistema, NO a ti. No hay respuestas correctas o incorrectas.
- Por favor, piensa en voz alta mientras completas las tareas.
- Puedes hacer preguntas, pero tal vez no pueda responderlas hasta después.
- La sesión durará aproximadamente {{session_duration}} minutos.
- Estaremos grabando la sesión para análisis. ¿Estás de acuerdo?

¿Tienes alguna pregunta antes de empezar?
```

---

### Pre-task Questions (5 minutes)

1. **{{pre_q1}}**
   - Purpose: {{pre_q1_purpose}}

2. **{{pre_q2}}**
   - Purpose: {{pre_q2_purpose}}

3. **{{pre_q3}}**
   - Purpose: {{pre_q3_purpose}}

---

### Task Execution (30-40 minutes)

**For each scenario:**

```
Escenario {{N}}: {{scenario_name}}

Imagina que {{scenario_context}}.

Tu tarea es: {{task_description}}

Por favor, piensa en voz alta mientras lo haces. Avísame cuando hayas terminado o si te atascas.

[START TIMER]
[OBSERVE: Note actions, errors, hesitations, comments]
[STOP TIMER when complete or after {{timeout}} minutes]

Post-task questions:
- ¿Cómo te sentiste completando esta tarea? (1-5)
- ¿Qué tan difícil fue? (1-5)
- ¿Algo te confundió o frustró?
- ¿Qué cambiarías?
```

**Probing Questions (use when needed):**
- "¿Qué estás pensando ahora?"
- "¿Por qué elegiste esa opción?"
- "¿Qué esperabas que pasara?"
- "¿Dónde buscarías esa información?"

---

### System Usability Scale (5 minutes)

Rate from 1 (Strongly Disagree) to 5 (Strongly Agree):

1. Creo que me gustaría usar este sistema frecuentemente
2. Encontré el sistema innecesariamente complejo
3. Pensé que el sistema era fácil de usar
4. Creo que necesitaría ayuda técnica para usar este sistema
5. Encontré que las funciones del sistema estaban bien integradas
6. Pensé que había demasiada inconsistencia en el sistema
7. Imagino que la mayoría de la gente aprendería a usar este sistema rápidamente
8. Encontré el sistema muy incómodo de usar
9. Me sentí muy seguro usando el sistema
10. Necesitaría aprender muchas cosas antes de poder usar este sistema

**SUS Score Calculation:**
```
Odd items (1,3,5,7,9): score - 1
Even items (2,4,6,8,10): 5 - score
Sum all and multiply by 2.5 = SUS Score (0-100)
```

**Interpretation:**
- 80-100: Grade A (Excellent)
- 68-79: Grade B (Good)
- 68: Average
- 51-67: Grade C (OK)
- 0-50: Grade F (Fail)

---

### Post-test Questions (5-10 minutes)

1. **Overall Impression:**
   - ¿Qué es lo que más te gustó del sistema?
   - ¿Qué es lo que más te frustró?

2. **Specific Features:**
   - {{post_q1}}
   - {{post_q2}}
   - {{post_q3}}

3. **Comparison (if applicable):**
   - ¿Cómo se compara con {{competitor}}?

4. **Suggestions:**
   - Si pudieras cambiar una cosa, ¿qué sería?

5. **Net Promoter Score:**
   - En una escala de 0-10, ¿qué tan probable es que recomiendes este producto a un colega?

---

### Closing (2 minutes)

```
Eso es todo! Muchas gracias por tu tiempo y feedback honesto.

Tus comentarios son muy valiosos y nos ayudarán a mejorar el producto.

¿Tienes alguna pregunta final para mí?

[Compensation details if applicable]

Que tengas un excelente día!
```

---

## 📈 Data Collection

### During Session

**Observation Notes:**
- Task completion (Yes/No)
- Time on task (seconds)
- Number of errors
- Number of clicks/actions
- Verbal comments (quotes)
- Facial expressions (frustration, delight)
- Hesitations (where user paused)

**Recording:**
- [ ] Screen recording
- [ ] Audio recording
- [ ] Webcam (optional)
- [ ] Click heatmap tool

---

### Data Collection Template

| Participant | Scenario | Completed | Time (s) | Errors | Clicks | Satisfaction | Comments |
|-------------|----------|-----------|----------|--------|--------|--------------|----------|
| P1 | 1 | Yes/No | | | | 1-5 | |
| P1 | 2 | Yes/No | | | | 1-5 | |
| P2 | 1 | Yes/No | | | | 1-5 | |
| ... | ... | ... | ... | ... | ... | ... | ... |

---

## 🔍 Analysis Framework

### Quantitative Analysis

**Task Completion Rate:**
```
Completion Rate = (Tasks Completed / Tasks Attempted) × 100
```

**Average Time on Task:**
```
Avg Time = Sum(Task Times) / Number of Participants
```

**Error Rate:**
```
Error Rate = (Total Errors / Total Actions) × 100
```

**SUS Score:**
```
SUS = ((Sum of odd items - 5) + (25 - Sum of even items)) × 2.5
```

---

### Qualitative Analysis

**Thematic Analysis:**
1. Transcribe verbal comments
2. Code comments by theme:
   - Navigation issues
   - Terminology confusion
   - Missing features
   - Positive feedback
   - Suggestions
3. Count frequency of each theme
4. Prioritize by:
   - Severity (High, Medium, Low)
   - Frequency (How many users mentioned)
   - Impact (Business/User impact)

---

### Issue Severity Classification

| Severity | Definition | Example |
|----------|------------|---------|
| **Critical** | Prevents task completion, no workaround | Cannot submit form (broken button) |
| **High** | Causes significant frustration, difficult workaround | Confusing error messages |
| **Medium** | Minor frustration, workaround exists | Non-optimal button placement |
| **Low** | Cosmetic issue, no impact on usability | Typo in label |

---

## 📊 Results Report Template

### Executive Summary

**Test Overview:**
- **Feature:** {{feature_name}}
- **Participants:** {{n}} users
- **Date:** {{test_date}}
- **Method:** {{test_method}}

**Key Findings:**
1. {{finding_1}}
2. {{finding_2}}
3. {{finding_3}}

**Overall SUS Score:** {{sus_score}}/100 ({{sus_grade}})

---

### Detailed Results

#### Task Performance

| Task | Completion Rate | Avg Time | Errors | SUS |
|------|----------------|----------|--------|-----|
| Scenario 1 | {{s1_completion}}% | {{s1_time}}s | {{s1_errors}} | {{s1_satisfaction}}/5 |
| Scenario 2 | {{s2_completion}}% | {{s2_time}}s | {{s2_errors}} | {{s2_satisfaction}}/5 |
| Scenario 3 | {{s3_completion}}% | {{s3_time}}s | {{s3_errors}} | {{s3_satisfaction}}/5 |
| **Overall** | {{overall_completion}}% | {{overall_time}}s | {{overall_errors}} | {{overall_satisfaction}}/5 |

#### Issues Identified

**Critical Issues (Must Fix):**
1. **{{critical_issue_1}}**
   - Affected: {{critical_issue_1_users}}/{{total_users}} users
   - Impact: {{critical_issue_1_impact}}
   - Recommendation: {{critical_issue_1_fix}}

**High Priority Issues:**
1. **{{high_issue_1}}**
   - Affected: {{high_issue_1_users}}/{{total_users}} users
   - Recommendation: {{high_issue_1_fix}}

#### Positive Feedback

Participants loved:
- {{positive_1}}
- {{positive_2}}
- {{positive_3}}

---

### Recommendations

**Immediate Actions:**
1. {{recommendation_1}}
2. {{recommendation_2}}
3. {{recommendation_3}}

**Future Iterations:**
1. {{future_1}}
2. {{future_2}}
3. {{future_3}}

**Next Steps:**
- [ ] {{next_step_1}}
- [ ] {{next_step_2}}
- [ ] {{next_step_3}}

---

## 🎨 Example: User Profile Test Plan

```markdown
# Usability Test Plan: User Profile Management

**Feature:** User Profile Management
**Test Date:** 2025-11-28
**Sample Size:** 6 participants
**Method:** Moderated remote (Zoom)

## Objectives
1. Validate that users can easily update their profile information
2. Test discoverability of advanced settings
3. Measure satisfaction with photo upload flow

## Scenario 1: Update Email Address
**Task:** "You've changed your email. Update your profile with your new email: newuser@example.com"

**Success Criteria:**
- ✅ User finds Settings page
- ✅ User locates Email field
- ✅ User receives confirmation message

**Expected Time:** 1-2 minutes

## Scenario 2: Upload Profile Photo
**Task:** "Add a profile photo to personalize your account"

**Success Criteria:**
- ✅ User finds upload button
- ✅ User successfully uploads image
- ✅ User sees preview before saving

**Expected Time:** 2-3 minutes

## Metrics
- **Target Completion:** 90%
- **Target Time:** <3 minutes per task
- **Target SUS:** >70

## Results (Example)
- **Completion Rate:** 83% (5/6 users)
- **Avg Time:** 2.4 minutes
- **SUS Score:** 72/100 (Grade B)

## Issues Found
1. **Critical:** Email field hidden in dropdown - 4/6 users couldn't find it
2. **High:** No feedback when photo is uploading - 5/6 users confused
3. **Medium:** Save button too far from form - 3/6 users didn't see it

## Recommendations
1. Move email to main settings view (not dropdown)
2. Add progress bar for photo upload
3. Add floating save button or sticky footer
```

---

## 📚 Related Documents

- **User Persona:** {{persona_link}}
- **User Journey Map:** {{journey_link}}
- **Wireframes:** {{wireframe_link}}
- **Prototype:** {{prototype_link}}

---

## ✅ Pre-Test Checklist

- [ ] Test plan reviewed and approved
- [ ] Participants recruited and screened
- [ ] Prototype/product ready and tested
- [ ] Test environment prepared (tool setup, recording)
- [ ] Facilitator trained on script
- [ ] Consent forms prepared
- [ ] Compensation/incentives ready
- [ ] Data collection templates prepared
- [ ] Backup plan if tech fails

---

**Template Version:** 1.0.0
**Last Updated:** 2025-11-24
**Created by:** mjcuadrado-net-sdk
**Estimated Time:** 2-3 hours to plan, 1 hour per participant, 4-6 hours analysis
